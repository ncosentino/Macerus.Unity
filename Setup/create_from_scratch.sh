echo "Cloning products repositories..."

mkdir products
mkdir products/Macerus

git clone https://github.com/ncosentino/Macerus.Unity.git ./products/Macerus/macerus-unity
git clone https://github.com/ncosentino/Macerus.git ./products/Macerus/macerus-game

echo "Cloning libraries repositories..."

mkdir libraries

git clone https://github.com/ncosentino/ProjectXyz.git ./libraries/projectxyz
git clone https://github.com/ncosentino/Tiled.Net.git ./libraries/tiled.net
git clone https://github.com/ncosentino/NexusLabs.Framework.git ./libraries/nexus