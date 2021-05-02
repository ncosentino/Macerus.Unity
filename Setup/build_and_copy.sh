echo "Building ProjectXyz..."
msbuild.exe "libraries/projectxyz/ProjectXyz/ProjectXyz.csproj" //t:Build //p:Configuration=Debug //verbosity:quiet //clp:ErrorsOnly
echo "ProjectXyz has finished building."

echo "Building Macerus..."
msbuild.exe "products/Macerus/macerus-game/Macerus/Macerus.csproj" //t:Build //p:Configuration=Debug //verbosity:quiet //clp:ErrorsOnly
echo "Macerus has finished building."

echo "Copying to Unity plugins directory..."
cp -u ./products/Macerus/macerus-game/Macerus/bin/Debug/net46/Tiled*.dll ./products/Macerus/macerus-unity/Assets/MacerusPlugins
cp -u ./products/Macerus/macerus-game/Macerus/bin/Debug/net46/Macerus*.dll ./products/Macerus/macerus-unity/Assets/MacerusPlugins
cp -u ./products/Macerus/macerus-game/Macerus/bin/Debug/net46/ProjectXyz*.dll ./products/Macerus/macerus-unity/Assets/MacerusPlugins
cp -u ./products/Macerus/macerus-game/Macerus/bin/Debug/net46/Nexus*.dll ./products/Macerus/macerus-unity/Assets/MacerusPlugins

echo "Done, my son. Summon unto me this glorious RPG."