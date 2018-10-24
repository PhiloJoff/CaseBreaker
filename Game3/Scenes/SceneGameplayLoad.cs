using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaseBreaker.Scenes
{
    class SceneGameplayLoad : SceneGameplay
    {
        private OpenFileDialog openFileDialog;
        public SceneGameplayLoad(MainGame mainGame) : base(mainGame)
        {

        }

        public override void Load()
        {
            base.Load();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Stage File|*.lvl";
            openFileDialog.Title = "Open a stage file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveEditor saveEditor;
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(openFileDialog.FileName)));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SaveEditor));
                saveEditor = (SaveEditor)serializer.ReadObject(stream);
                saveEditor.ConvertToRead(ref mapInt);
            }
            mesBricks = GenerateMap(mapInt);
        }
    }
}
