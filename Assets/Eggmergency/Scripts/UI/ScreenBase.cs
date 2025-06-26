using UnityEngine;

namespace Eggmergency.Scripts.UI
{
    public class ScreenBase:MonoBehaviour
    {
        [SerializeField]private GameObject _holder;
        public virtual void Show()
        {
            _holder.SetActive(true);
        }

        public virtual void Hide()
        {
            _holder.SetActive(false);

        }
    }
}