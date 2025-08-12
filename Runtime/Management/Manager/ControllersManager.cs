using UnityEngine;

namespace ProjectTemplate
{
    public partial class ControllersManager : Manager<ControllersManager>
    {
        #region Public Methods

        public partial void LoadController(GameScene scene)
        {
            if (scene == null || scene.Controllers.Count == 0)
            {
                return;
            }

            GameObject folder = new(Format.Polish(nameof(_controllers)));

            _controllers ??= new();

            foreach (Controller controller in scene.Controllers)
            {
                Controller obj = Instantiate(controller, folder.transform);

                obj.name = controller.name;

                _controllers.Add(obj);
            }
        }

        #endregion
    }
}