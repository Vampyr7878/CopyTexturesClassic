using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Copy_Textures
{
    class Program
    {
        static Items items;
        static XmlSerializer xml;
        static string path = @"C:\Users\wojte\Documents\Visual Studio 2013\Projects\WoW Character Viewer Classic\WoW Character Viewer Classic\Data\";

        static void Main(string[] args)
        {
            CopyObjectComponents("BackItems.xml", "Cape");
            CopyTextureComponents("ClothChestItems.xml");
            CopyTextureComponents("LeatherChestItems.xml");
            CopyTextureComponents("MailChestItems.xml");
            CopyTextureComponents("PlateChestItems.xml");
            CopyTextureComponents("ClothWristItems.xml");
            CopyTextureComponents("LeatherWristItems.xml");
            CopyTextureComponents("MailWristItems.xml");
            CopyTextureComponents("PlateWristItems.xml");
            CopyTextureComponents("ClothHandsItems.xml");
            CopyTextureComponents("LeatherHandsItems.xml");
            CopyTextureComponents("MailHandsItems.xml");
            CopyTextureComponents("PlateHandsItems.xml");
            CopyTextureComponents("ClothWaistItems.xml");
            CopyTextureComponents("LeatherWaistItems.xml");
            CopyTextureComponents("MailWaistItems.xml");
            CopyTextureComponents("PlateWaistItems.xml");
            CopyTextureComponents("ClothLegsItems.xml");
            CopyTextureComponents("LeatherLegsItems.xml");
            CopyTextureComponents("MailLegsItems.xml");
            CopyTextureComponents("PlateLegsItems.xml");
            CopyTextureComponents("ClothFeetItems.xml");
            CopyTextureComponents("LeatherFeetItems.xml");
            CopyTextureComponents("MailFeetItems.xml");
            CopyTextureComponents("PlateFeetItems.xml");
            CopyTextureComponents("ShirtItems.xml");
            CopyTextureComponents("TabardItems.xml");
            CopyObjectComponents("ClothHeadItems.xml", "Head");
            CopyObjectComponents("LeatherHeadItems.xml", "Head");
            CopyObjectComponents("MailHeadItems.xml", "Head");
            CopyObjectComponents("PlateHeadItems.xml", "Head");
            CopyObjectComponents("ClothShoulderItems.xml", "Shoulder");
            CopyObjectComponents("LeatherShoulderItems.xml", "Shoulder");
            CopyObjectComponents("MailShoulderItems.xml", "Shoulder");
            CopyObjectComponents("PlateShoulderItems.xml", "Shoulder");
            CopyObjectComponents("DaggerItems.xml", "Weapon");
            CopyObjectComponents("FistWeaponItems.xml", "Weapon");
            CopyObjectComponents("OneHandedAxeItems.xml", "Weapon");
            CopyObjectComponents("OneHandedMaceItems.xml", "Weapon");
            CopyObjectComponents("OneHandedSwordItems.xml", "Weapon");
            CopyObjectComponents("PolearmItems.xml", "Weapon");
            CopyObjectComponents("StaffItems.xml", "Weapon");
            CopyObjectComponents("TwoHandedAxeItems.xml", "Weapon");
            CopyObjectComponents("TwoHandedMaceItems.xml", "Weapon");
            CopyObjectComponents("TwoHandedSwordItems.xml", "Weapon");
            CopyObjectComponents("BowItems.xml", "Weapon");
            CopyObjectComponents("CrossbowItems.xml", "Weapon");
            CopyObjectComponents("GunItems.xml", "Weapon");
            CopyObjectComponents("ThrownItems.xml", "Weapon");
            CopyObjectComponents("WandItems.xml", "Weapon");
            CopyObjectComponents("ShieldItems.xml", "Shield");
            CopyObjectComponents("HeldInOffHandItems.xml", "Weapon");
            CopyMountTextures("MountItems.xml");
        }

        static void CopyObjectComponents(string file, string type)
        {
            xml = new XmlSerializer(typeof(Items));
            using(StreamReader reader = new StreamReader(path + file))
            {
                items = (Items)xml.Deserialize(reader);
            }
            foreach(ItemsItem item in items.Item)
            {
                CopyObjectComponent(item.Textures.Left, type);
                CopyObjectComponent(item.Textures.Right, type);
            }
        }

        static void CopyObjectComponent(string texture, string type)
        {
            string source = @"G:\MPQ\Item\ObjectComponents\" + type + @"\" + texture + ".png";
            string destination = @"C:\Users\wojte\Documents\Visual Studio 2013\Projects\WoW Character Viewer Classic\WoW Character Viewer Classic\Item\ObjectComponents\" + type + @"\" + texture + ".png";
            Copy(texture, source, destination);
        }

        static void CopyTextureComponents(string file)
        {
            xml = new XmlSerializer(typeof(Items));
            using (StreamReader reader = new StreamReader(path + file))
            {
                items = (Items)xml.Deserialize(reader);
            }
            foreach (ItemsItem item in items.Item)
            {
                CopyTextureComponent(item.Textures.ArmUpper, "ArmUpper");
                CopyTextureComponent(item.Textures.ArmLower, "ArmLower");
                CopyTextureComponent(item.Textures.Hand, "Hand");
                CopyTextureComponent(item.Textures.TorsoUpper, "TorsoUpper");
                CopyTextureComponent(item.Textures.TorsoLower, "TorsoLower");
                CopyTextureComponent(item.Textures.LegUpper, "LegUpper");
                CopyTextureComponent(item.Textures.LegLower, "LegLower");
                CopyTextureComponent(item.Textures.Foot, "Foot");
            }
        }

        static void CopyTextureComponent(string texture, string part)
        {
            string source = @"G:\MPQ\Item\TextureComponents\" + part + @"Texture\" + texture + "_U.png";
            string destination = @"C:\Users\wojte\Documents\Visual Studio 2013\Projects\WoW Character Viewer Classic\WoW Character Viewer Classic\Item\TextureComponents\" + part + @"Texture\" + texture + "_U.png";
            if(!Copy(texture, source, destination))
            {
                source = source.Replace("_U", "_M");
                destination = destination.Replace("_U", "_M");
                Copy(texture, source, destination);
                source = source.Replace("_M", "_F");
                destination = destination.Replace("_M", "_F");
                Copy(texture, source, destination);
            }
        }

        static void CopyMountTextures(string file)
        {
            xml = new XmlSerializer(typeof(Items));
            using(StreamReader reader = new StreamReader(path + file))
            {
                items = (Items)xml.Deserialize(reader);
            }
            foreach(ItemsItem item in items.Item)
            {
                CopyMountTexture(item.Textures.Left);
            }
        }

        static void CopyMountTexture(string texture)
        {
            string source = @"G:\MPQ\Creature\" + texture + ".png";
            string destination = @"C:\Users\wojte\Documents\Visual Studio 2013\Projects\WoW Character Viewer Classic\WoW Character Viewer Classic\Creature\" + texture + ".png";
            string[] path = destination.Split('\\');
            string directory = destination.Replace("\\" + path.Last(), "");
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            Copy(texture, source, destination);
        }

        static bool Copy(string texture, string source, string destination)
        {
            if(File.Exists(source))
            {
                File.Copy(source, destination, true);
                Console.WriteLine(texture + " copied");
                return true;
            }
            return false;
        }
    }
}
