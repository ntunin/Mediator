using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public class BodyKeyboardEventHandlerBinderBuilder : Builder
    {
        public BodyKeyboardEventHandlerBinderBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            IKeyboardEventProvider eventProvider = null;
            Dictionary<List<Keys>, BodyKeyboardEventHandler> handlers = null;
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Instance", (object o)=>{ eventProvider = (IKeyboardEventProvider)DI.Get((string)o); } },
                {"Actions", (object o)=>{ handlers = ParseHandlers(o); } }
            });
            return new BodyKeyboardEventHandlerBinder(eventProvider, handlers);
        }

        private Dictionary<List<Keys>, BodyKeyboardEventHandler> ParseHandlers(object actionsDescription)
        {
            List<object> actionsDescriptions = (List<object>)actionsDescription;
            Dictionary<List<Keys>, BodyKeyboardEventHandler> result = new Dictionary<List<Keys>, BodyKeyboardEventHandler>();
            foreach(object description in actionsDescriptions)
            {
                Dictionary<string, object> actionDescription = (Dictionary<string, object>)description;
                List<Keys> keys = ParseKeys(actionDescription);
                BodyKeyboardEventHandler handler = (BodyKeyboardEventHandler)DI.Get((string)actionDescription["Control"]);
                result[keys] = handler;
            }
            return result;
        }

        private List<Keys> ParseKeys(Dictionary<string, object> actionDescription)
        {
            if(!actionDescription.ContainsKey("Keys"))
            {
                return null;
            }
            List<object> keysDescriptions = (List<object>)actionDescription["Keys"];
            List<Keys> result = new List<Keys>(); 
            foreach(object keyDescription in keysDescriptions)
            {
                Keys key = ParseKey((string)keyDescription);
                result.Add(key);
            }
            return result;
        }

        private Keys ParseKey(string name)
        {
            return new Dictionary<string, Keys>
            {
                {"A", Keys.A },
                {"Add", Keys.Add },
                {"Alt", Keys.Alt },
                {"Apps", Keys.Apps },
                {"Attn", Keys.Attn },
                {"B", Keys.B },
                {"Back", Keys.Back },
                {"BrowserBack", Keys.BrowserBack },
                {"BrowserFavorites", Keys.BrowserFavorites },
                {"BrowserForward", Keys.BrowserForward },
                {"BrowserHome", Keys.BrowserHome },
                {"BrowserRefresh", Keys.BrowserRefresh },
                {"BrowserSearch", Keys.BrowserSearch },
                {"BrowserStop", Keys.BrowserStop },
                {"C", Keys.C},
                {"Cancel", Keys.Cancel },
                {"Capital", Keys.Capital },
                {"CapsLock", Keys.CapsLock },
                {"Clear", Keys.Clear },
                {"Control", Keys.Control },
                {"ControlKey", Keys.ControlKey },
                {"Crsel", Keys.Crsel },
                {"D", Keys.D },
                {"D0", Keys.D0 },
                {"D1", Keys.D1 },
                {"D2", Keys.D2 },
                {"D3", Keys.D3 },
                {"D4", Keys.D4 },
                {"D5", Keys.D5 },
                {"D6", Keys.D6 },
                {"D7", Keys.D7 },
                {"D8", Keys.D8 },
                {"D9", Keys.D9 },
                {"Decimal", Keys.Decimal },
                {"Delete", Keys.Delete },
                {"Divide", Keys.Divide },
                {"Down", Keys.Down },
                {"E", Keys.E },
                {"End", Keys.End },
                {"Enter", Keys.Enter },
                {"EraseEof", Keys.EraseEof },
                {"Escape", Keys.Escape },
                {"Execute", Keys.Execute },
                {"Exsel", Keys.Exsel },
                {"F", Keys.F },
                {"F1", Keys.F1 },
                {"F10", Keys.F10 },
                {"F11", Keys.F11 },
                {"F12", Keys.F12 },
                {"F13", Keys.F13 },
                {"F14", Keys.F14 },
                {"F15", Keys.F15 },
                {"F16", Keys.F16 },
                {"F17", Keys.F17 },
                {"F18", Keys.F18 },
                {"F19", Keys.F19 },
                {"F2", Keys.F2 },
                {"F20", Keys.F20 },
                {"F21", Keys.F21 },
                {"F22", Keys.F22 },
                {"F23", Keys.F23 },
                {"F24", Keys.F24 },
                {"F3", Keys.F3 },
                {"F4", Keys.F4 },
                {"F5", Keys.F5 },
                {"F6", Keys.F6 },
                {"F7", Keys.F7 },
                {"F8", Keys.F8 },
                {"F9", Keys.F9 },
                {"FinalMode", Keys.FinalMode },
                {"G", Keys.G },
                {"H", Keys.H },
                {"", Keys.HanguelMode },
                {"HanguelMode", Keys.HangulMode },
                {"HanjaMode", Keys.HanjaMode },
                {"Help", Keys.Help },
                {"Home", Keys.Home },
                {"I", Keys.I },
                {"IMEAccept", Keys.IMEAccept },
                {"IMEAceept", Keys.IMEAceept },
                {"IMEConvert", Keys.IMEConvert },
                {"IMEModeChange", Keys.IMEModeChange },
                {"IMENonconvert", Keys.IMENonconvert },
                {"Insert", Keys.Insert },
                {"J", Keys.J },
                {"JunjaMode", Keys.JunjaMode },
                {"K", Keys.K },
                {"KanaMode", Keys.KanaMode },
                {"KanjiMode", Keys.KanjiMode },
                {"KeyCode", Keys.KeyCode },
                {"L", Keys.L },
                {"LaunchApplication1", Keys.LaunchApplication1 },
                {"LaunchApplication2", Keys.LaunchApplication2 },
                {"LaunchMail", Keys.LaunchMail },
                {"LButton", Keys.LButton },
                {"LControlKey", Keys.LControlKey },
                {"Left", Keys.Left },
                {"LMenu", Keys.LMenu },
                {"LShiftKey", Keys.LShiftKey },
                {"LWin", Keys.LWin },
                {"M", Keys.M },
                {"MButton", Keys.MButton },
                {"MediaNextTrack", Keys.MediaNextTrack },
                {"MediaPlayPause", Keys.MediaPlayPause },
                {"MediaPreviousTrack", Keys.MediaPreviousTrack },
                {"MediaStop", Keys.MediaStop },
                {"Menu", Keys.Menu },
                {"Modifiers", Keys.Modifiers },
                {"Multiply", Keys.Multiply },
                {"N", Keys.N },
                {"Next", Keys.Next },
                {"NoName", Keys.NoName },
                {"None", Keys.None },
                {"NumLock", Keys.NumLock },
                {"NumPad0", Keys.NumPad0 },
                {"NumPad1", Keys.NumPad1 },
                {"NumPad2", Keys.NumPad2 },
                {"NumPad3", Keys.NumPad3 },
                {"NumPad4", Keys.NumPad4 },
                {"NumPad5", Keys.NumPad5 },
                {"NumPad6", Keys.NumPad6 },
                {"NumPad7", Keys.NumPad7 },
                {"NumPad8", Keys.NumPad8 },
                {"NumPad9", Keys.NumPad9 },
                {"O", Keys.O },
                {"Oem1", Keys.Oem1 },
                {"Oem102", Keys.Oem102 },
                {"Oem2", Keys.Oem2 },
                {"Oem3", Keys.Oem3 },
                {"Oem4", Keys.Oem4 },
                {"Oem5", Keys.Oem5 },
                {"Oem6", Keys.Oem6 },
                {"Oem7", Keys.Oem7 },
                {"Oem8", Keys.Oem8 },
                {"OemBackslash", Keys.OemBackslash },
                {"OemClear", Keys.OemClear },
                {"OemCloseBrackets", Keys.OemCloseBrackets },
                {"Oemcomma", Keys.Oemcomma },
                {"OemMinus", Keys.OemMinus },
                {"OemOpenBrackets", Keys.OemOpenBrackets },
                {"OemPeriod", Keys.OemPeriod },
                {"OemPipe", Keys.OemPipe },
                {"Oemplus", Keys.Oemplus },
                {"OemQuestion", Keys.OemQuestion },
                {"OemQuotes", Keys.OemQuotes },
                {"OemSemicolon", Keys.OemSemicolon },
                {"Oemtilde", Keys.Oemtilde },
                {"P", Keys.P },
                {"Pa1", Keys.Pa1 },
                {"Packet", Keys.Packet },
                {"PageDown", Keys.PageDown },
                {"PageUp", Keys.PageUp },
                {"Pause", Keys.Pause },
                {"Play", Keys.Play },
                {"Print", Keys.Print },
                {"PrintScreen", Keys.PrintScreen },
                {"Prior", Keys.Prior },
                {"ProcessKey", Keys.ProcessKey },
                {"Q", Keys.Q },
                {"R", Keys.R },
                {"RButton", Keys.RButton },
                {"RControlKey", Keys.RControlKey },
                {"Return", Keys.Return },
                {"Right", Keys.Right },
                {"RMenu", Keys.RMenu },
                {"RShiftKey", Keys.RShiftKey },
                {"RWin", Keys.RWin },
                {"S", Keys.S },
                {"Scroll", Keys.Scroll },
                {"Select", Keys.Select },
                {"SelectMedia", Keys.SelectMedia },
                {"Shift", Keys.Shift },
                {"ShiftKey", Keys.ShiftKey },
                {"Sleep", Keys.Sleep },
                {"Snapshot", Keys.Snapshot },
                {"Space", Keys.Space },
                {"Subtract", Keys.Subtract },
                {"T", Keys.T },
                {"Tab", Keys.Tab },
                {"U", Keys.U },
                {"Up", Keys.Up },
                {"V", Keys.V },
                {"VolumeDown", Keys.VolumeDown },
                {"VolumeMute", Keys.VolumeMute },
                {"VolumeUp", Keys.VolumeUp },
                {"W", Keys.W },
                {"X", Keys.X },
                {"XButton1", Keys.XButton1 },
                {"XButton2", Keys.XButton2 },
                {"Y", Keys.Y },
                {"Z", Keys.Z },
                {"Zoom", Keys.Zoom }
            }[name];
        }
    }
}
