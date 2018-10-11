using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    public class GameState
    {
        public enum SceneType {
            MainMenu,
            GameOver,
            Gameplay,
            Pause,
            Win,
            GameEditor
        }
        protected MainGame MainGame;
        public Scene CurrentScene {get; protected set;}

        public GameState(MainGame mainGame)
        {
            this.MainGame = mainGame;
        }

        public void SwitchScene(SceneType sceneType)
        {
            if(CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            switch (sceneType)
            {
                case SceneType.MainMenu:
                    CurrentScene = new SceneMainMenu(MainGame);
                    break;
                case SceneType.GameOver:
                    CurrentScene = new SceneGameOver(MainGame);
                    break;
                case SceneType.Gameplay:
                    CurrentScene = new SceneGameplay(MainGame);
                    break;
                case SceneType.Pause:
                    CurrentScene = new ScenePause(MainGame);
                    break;
                case SceneType.Win:
                    CurrentScene = new SceneWin(MainGame);
                    break;
                case SceneType.GameEditor:
                    CurrentScene = new SceneGameEditor(MainGame);
                    break;
                default:
                    break;
            }
        }
    }
}
