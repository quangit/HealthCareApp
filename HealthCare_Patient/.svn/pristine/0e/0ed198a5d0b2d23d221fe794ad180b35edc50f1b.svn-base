//Updated to v 1.4.6392 

using Xamarin.Forms;

namespace HealthCare.Behaviors
{
    public abstract class Behavior : BindableObject, IBehavior
    {
        public virtual BindableObject AssociatedObject { get; private set; }

        public virtual void Detach()
        {
            OnDetach();
            AssociatedObject = null;
        }

        public virtual void Attach(BindableObject dependencyObject)
        {
            AssociatedObject = dependencyObject;
            OnAttach();
        }

        protected abstract void OnAttach();
        protected abstract void OnDetach();
    }
}