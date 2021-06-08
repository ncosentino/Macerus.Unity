1. If you haven't already cloned everything down, you'll need to do this initial step:
   - Run `create_from_scratch.sh`. You will want to run this script from the ROOT of where you want your code to live. This is because it creates a directory structure of what we'll be working with.
   - PLEASE NOTE: this script pulls from github which is the MIRROR of the code. It is subject to being slightly out of date. Please use BitBucket for ultimate access.
2. Next we need to build & copy all of the DLLs for our projects into Unity
   - NOTE: we only need to do this step once initially. After that, we have a 'Macerus Tools' menu in Unity that can do this for you.
   - From the root of your code folder (i.e. direct children should be 'libraries' and 'products' directories) run `build_and_copy.sh`.
3. You will need to setup the MySQL DB.
   - You'll want it to be called "macerus" with a user/pass as both 'macerus'.
   - Please use 'create-db.sql' for the schema+data which can be found in 'ROOT\products\macerus\macerus-game\Macerus.Plugins.Content\Data'.
4. Within Unity, you should see a "Macerus" menu and a "Dependencies" sub menu.
5. From "Macerus/Dependencies" select "Build+Copy Dependencies"
6. Summon the glory.