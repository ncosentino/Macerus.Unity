using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Unity.Threading;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    using Resources = UnityEngine.Resources;

    public sealed class ResourceLoader : IResourceLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IDispatcher _dispatcher;

        public ResourceLoader(
            ICoroutineRunner coroutineRunner,
            IDispatcher dispatcher)
        {
            _coroutineRunner = coroutineRunner;
            _dispatcher = dispatcher;
        }

        public string LoadText(string relativeResourcePath)
        {
            var asset = Load<TextAsset>(relativeResourcePath);
            return asset.text;
        }

        public async Task<byte[]> LoadBytesAsync(string relativeResourcePath)
        {
            var asset = await LoadAsync<TextAsset>(relativeResourcePath);
            if (_dispatcher.IsMainThread)
            {
                return asset.bytes;
            }

            // we can only pull unity resource related data from the main thread.
            byte[] bytes = null;
            _dispatcher.RunOnMainThread(() => bytes = asset.bytes);

            await Task.Run(() =>
            {
                while (bytes == null)
                {
                    System.Threading.Thread.Sleep(0);
                }
            });

            return bytes;
        }

        public async Task<Stream> LoadStreamAsync(string relativeResourcePath)
        {
            var bytes = await LoadBytesAsync(relativeResourcePath);
            return new MemoryStream(bytes);
        }

        public IReadOnlyCollection<TResource> LoadAll<TResource>(string relativeResourcePath)
            where TResource : UnityEngine.Object
        {
            var resources = Resources.LoadAll<TResource>(relativeResourcePath);
            if (resources == null || !resources.Any())
            {
                throw new InvalidOperationException(
                    $"No resources with relative resource path " +
                    $"'{relativeResourcePath}' could be loaded.");
            }

            return resources;
        }

        public TResource Load<TResource>(string relativeResourcePath)
            where TResource : UnityEngine.Object
        {
            var uncasted = Resources.Load(relativeResourcePath);
            if (uncasted == null)
            {
                throw new InvalidOperationException(
                    $"No resource with relative resource path " +
                    $"'{relativeResourcePath}' could be loaded.");
            }

            var casted = uncasted as TResource;
            if (casted == null)
            {
                throw new InvalidOperationException(
                    $"Resource with relative resource path " +
                    $"'{relativeResourcePath}' was of type '{uncasted.GetType()}' " +
                    $"and not of type '{typeof(TResource)}'.");
            }

            return casted;
        }

        public async Task<TResource> LoadAsync<TResource>(string relativeResourcePath)
            where TResource : UnityEngine.Object
        {
            // FIXME: this is a filthy hack because of places that use Task.Result
            if (_dispatcher.IsMainThread)
            {
                return Load<TResource>(relativeResourcePath);
            }

            TResource coroutineResult = null;
            Exception coroutineError = null;
            _coroutineRunner.StartCoroutine(
                LoadResourceCoroutine<TResource>(
                    relativeResourcePath,
                    r => coroutineResult = r),
                ex => coroutineError = ex);

            await Task.Run(() =>
            {
                while (coroutineResult == null && coroutineError == null)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }).ConfigureAwait(false);

            if (coroutineError != null)
            {
                throw new InvalidOperationException(
                    $"Could not load resource with relative path " +
                    $"'{relativeResourcePath}'. See inner exception for details.",
                    coroutineError);
            }

            return coroutineResult;
        }

        private IEnumerator LoadResourceCoroutine<TResource>(
            string relativeResourcePath,
            Action<TResource> callback)
            where TResource : UnityEngine.Object
        {
            var request = Resources.LoadAsync<UnityEngine.Object>(relativeResourcePath);
            while (!request.isDone)
            {
                yield return null;
            }

            var uncasted = request.asset;
            if (uncasted == null)
            {
                throw new InvalidOperationException(
                    $"No resource with relative resource path " +
                    $"'{relativeResourcePath}' could be loaded.");
            }

            var casted = uncasted as TResource;
            if (casted == null)
            {
                throw new InvalidOperationException(
                    $"Resource with relative resource path " +
                    $"'{relativeResourcePath}' was of type '{uncasted.GetType()}' " +
                    $"and not of type '{typeof(TResource)}'.");
            }

            callback(casted);
        }
    }
}
