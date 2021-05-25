using System;
using System.IO;
using System.Text;

using Assets.ContentCreator.MapEditor.Editor;
using Assets.Scripts.Autofac;

using Autofac;

using NUnit.Framework;

namespace Assets.Tests.EditorModeTests.ContentCreator.MapEditor
{
    public sealed class MapEditorTests
    {
        private static readonly IContainer _container;
        private static readonly IMapEditor _mapEditor;

        static MapEditorTests()
        {
            _container = new MacerusContainerBuilder().CreateContainer();
            _mapEditor = _container.Resolve<IMapEditor>();
        }

        [Test]
        public void SaveAndLoad_SwampMapSaveLoadSave_ConsistentSavedJson()
        {
            _mapEditor.LoadMap(
                @"Assets\Resources\Mapping\Maps\swamp.json",
                p => File.OpenRead(p));

            byte[] serializedMapBytes;
            byte[] serializedObjectBytes;
            using (var serializedMapStream = new MemoryStream())
            using (var serializedObjectStream = new MemoryStream())
            {
                _mapEditor.SaveMap(
                    @"map.json",
                    p => p.IndexOf(".objects.", StringComparison.OrdinalIgnoreCase) == -1
                        ? serializedMapStream
                        : serializedObjectStream);
                serializedMapBytes = serializedMapStream.ToArray();
                serializedObjectBytes = serializedObjectStream.ToArray();
            }

            var originalSerializedMapJson = Encoding.UTF8.GetString(serializedMapBytes);
            var originalSerializedObjectJson = Encoding.UTF8.GetString(serializedObjectBytes);

            using (var deserializedMapStream = new MemoryStream(serializedMapBytes))
            using (var deserializedObjectStream = new MemoryStream(serializedObjectBytes))
            {
                _mapEditor.LoadMap(
                    @"map.json",
                    p => p.IndexOf(".objects.", StringComparison.OrdinalIgnoreCase) == -1
                        ? deserializedMapStream
                        : deserializedObjectStream);
            }

            byte[] finalSerializedMapBytes;
            byte[] finalSerializedObjectBytes;
            using (var finalSerializedMapStream = new MemoryStream())
            using (var finalSerializedObjectStream = new MemoryStream())
            {
                _mapEditor.SaveMap(
                    @"map.json",
                    p => p.IndexOf(".objects.", StringComparison.OrdinalIgnoreCase) == -1
                        ? finalSerializedMapStream
                        : finalSerializedObjectStream);
                finalSerializedMapBytes = finalSerializedMapStream.ToArray();
                finalSerializedObjectBytes = finalSerializedObjectStream.ToArray();
            }

            var finalSerializedMapJson = Encoding.UTF8.GetString(finalSerializedMapBytes);
            var finalSerializedObjectJson = Encoding.UTF8.GetString(finalSerializedObjectBytes);

            Assert.AreEqual(originalSerializedMapJson, finalSerializedMapJson);
            Assert.AreEqual(originalSerializedObjectJson, finalSerializedObjectJson);
        }
    }
}