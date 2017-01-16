using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class CenterRelativeLayout : RelativeLayout
    {
        /// <summary>
        ///     Add a view to center of this relativelayout
        /// </summary>
        /// <param name="view"></param>
        /// <param name="widthRatio">
        ///     the ratio between width of view and width of relativelayout
        /// </param>
        /// <param name="heightRatio">
        ///     the ratio between height of view and height of relativelayout
        /// </param>
        public CenterRelativeLayout AddCenter(View view, float widthRatio, float heightRatio, double deltaX = 0,
            double deltaY = 0)
        {
            Children.Add(view,
                Constraint.RelativeToParent(parent => { return parent.Width*(1 - widthRatio)/2 + deltaX; }),
                Constraint.RelativeToParent(parent => { return parent.Height*(1 - heightRatio)/2 + deltaY; }),
                Constraint.RelativeToParent(parent => { return parent.Width*widthRatio; }),
                Constraint.RelativeToParent(parent => { return parent.Height*heightRatio; })
                );
            return this;
        }
    }
}