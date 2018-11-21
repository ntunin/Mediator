using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    public class PerspectiveCamera: Camera
    {
        private float angle;
        private Control control;
        private float near;
        private float far;
        private Vector3 position;
        private Vector3 target;
        private Vector3 up;



        public PerspectiveCamera(float angle, Control control, float near, float far, Vector3 position, Vector3 target, Vector3 up)
        {
            this.angle = angle;
            this.control = control;
            this.near = near;
            this.far = far;
            this.position = position;
            this.target = target;
            this.up = up;
        }
        
        public override void Setup()
        {
            Device device = (Device)DI.Get(DIConfigs.Device);
            device.Transform.Projection = Matrix.PerspectiveFovLH(angle, 1, near, far);
            device.Transform.View = Matrix.LookAtLH(position, target, up);
        }
    }
}
