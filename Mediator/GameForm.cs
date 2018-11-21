using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;

namespace Mediator
{
    public class GameForm: Form, IKeyboardEventProvider, IMouseEventProvider
    {

        protected Device device = null;
        protected Color cleanColor = Color.Azure;
        protected Body world;
        protected Scene scene;

        private string worldName;
        private string sceneName;
        private Stopwatch stopwatch;
        private float elapsedTime;
        private Dictionary<Keys, List<IKeyboardEventHandler>> keyHandlers = new Dictionary<Keys, List<IKeyboardEventHandler>>();
        private Dictionary<string, List<IMouseEventHandler>> mouseHandlers = new Dictionary<string, List<IMouseEventHandler>>();

        public GameForm()
        {
        }

        public GameForm(ControlStyles controlStyles, Color cleanColor, string worldName, string sceneName)
        {
            this.worldName = worldName;
            this.sceneName = sceneName;
            this.cleanColor = cleanColor;
            DI.Set(DIConfigs.RenderControl, this);
            SetStyle(controlStyles, true);
        }

        public virtual void InitializeGraphics()
        {
            device = (Device)DI.Get(DIConfigs.Device);
            world = (Body)DI.Get(worldName);
            scene = (Scene)DI.Get(sceneName);
            device.DeviceReset += new EventHandler(OnDeviceReset);
            device.DeviceResizing += new CancelEventHandler(OnCancelResize);
            OnDeviceReset(device, null);
            SetupMouseEvents();
        }

        public void StartGame()
        {
            stopwatch = Stopwatch.StartNew();
        }

        private void StopGame()
        {
            stopwatch.Stop();
            stopwatch = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (stopwatch == null || !stopwatch.IsRunning)
            {
                return;
            }
            float deltaTime = CalculateNewDeltaTime();
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, cleanColor, 1.0f, 0);
            device.BeginScene();
            scene.Present(deltaTime);
            device.EndScene();
            device.Present();
            Invalidate();
        }

        private float CalculateNewDeltaTime()
        {
            float elapsedTime = stopwatch.ElapsedMilliseconds;
            float deltaTime = elapsedTime - this.elapsedTime;
            this.elapsedTime = elapsedTime;
            return deltaTime/1000;
        }

        private void OnDeviceReset(object sender, EventArgs e)
        {
            scene.Prepare();
        }

        private void OnCancelResize(object sender,  CancelEventArgs e)
        {
            e.Cancel = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(!keyHandlers.ContainsKey(e.KeyCode))
            {
                return;
            }
            List<IKeyboardEventHandler> handlers = keyHandlers[e.KeyCode];
            foreach(IKeyboardEventHandler handler in handlers)
            {
                handler.HandleEvent(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        public void AddHandler(Keys key, IKeyboardEventHandler handler)
        {
            if (!keyHandlers.ContainsKey(key))
            {
                keyHandlers[key] = new List<IKeyboardEventHandler>();
            }
            List<IKeyboardEventHandler> handlers = keyHandlers[key];
            handlers.Add(handler);
        }

        public void RemoveHandler(Keys key, IKeyboardEventHandler handler)
        {
            if (!keyHandlers.ContainsKey(key))
            {
                return;
            }
            List<IKeyboardEventHandler> handlers = keyHandlers[key];
            handlers.Remove(handler);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(!mouseHandlers.ContainsKey("MouseMove"))
            {
                return;
            }
            List<IMouseEventHandler> handlers = mouseHandlers["MouseMove"];
            foreach(IMouseEventHandler handler in handlers)
            {
                handler.HandleEvent(e);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        public void AddAction(string key, IMouseEventHandler handler)
        {
            if (!mouseHandlers.ContainsKey(key))
            {
                mouseHandlers[key] = new List<IMouseEventHandler>();
            }
            List<IMouseEventHandler> actions = mouseHandlers[key];
            actions.Add(handler);
        }

        public void RemoveAction(string key, IMouseEventHandler handler)
        {
            List<IMouseEventHandler> actions = mouseHandlers[key];
            if (!mouseHandlers.ContainsKey(key))
            {
                return;
            }
            actions.Remove(handler);
        }

        private void SetupMouseEvents()
        {
            Control control = (Control)DI.Get(DIConfigs.RenderControl);
            if(control == this)
            {
                return;
            }

            control.MouseMove += new MouseEventHandler(OnMouseMove);
        }
    }
}
