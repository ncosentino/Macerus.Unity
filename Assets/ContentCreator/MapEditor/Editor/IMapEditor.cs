using System;
using System.IO;

namespace Assets.ContentCreator.MapEditor.Editor
{
    public interface IMapEditor
    {
        void ClearCurrentMap();

        void LoadMap(
            string mapPathToLoad,
            Func<string, Stream> openNewReadableStreamCallback);
        
        void SaveMap(
            string mapPathToSave,
            Func<string, Stream> openNewWriteableStreamCallback);
    }
}