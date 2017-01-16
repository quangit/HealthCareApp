using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.DependencyServices
{
    public interface IPhotoEdit
    {
        void LaunchEditorControl(byte[] imageByteArray);
        byte[] CorrectOrientation(byte[] imageByteArray);
    }
}
