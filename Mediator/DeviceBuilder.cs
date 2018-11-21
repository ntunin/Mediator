using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Windows.Forms;

namespace Mediator
{
    class DeviceBuilder: Builder
    {
        public DeviceBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            int adapter = ParseConfigInt("Adapter");
            DeviceType deviceType = ParseDeviceType();
            Control control = ParseControl();
            CreateFlags flags = ParseCreateFlags();
            PresentParameters parameters = ParsePresentParams();
            Device device = new Device(adapter, deviceType, control, flags, parameters);
            return device;
        }

        private DeviceType ParseDeviceType()
        {
            string value = (string)configs["DeviceType"];
            return (new Dictionary<string, DeviceType> {
                {"Hardware", DeviceType.Hardware},
                {"Software", DeviceType.Software},
                {"Reference", DeviceType.Reference},
                {"NullReference", DeviceType.NullReference},
            })[value];
        }

        private Control ParseControl()
        {
            string controlName = (string)configs["Control"];
            Control control = (Control)DI.Get(controlName);
            return control;
        }

        private CreateFlags ParseCreateFlags()
        {
            Dictionary<string, object> flagDescriptions = (Dictionary<string, object>)configs["CreateFlags"];
            CreateFlags flags = 0;
            int deviceOrdinal = Manager.Adapters.Default.Adapter;
            Caps caps = Manager.GetDeviceCaps(deviceOrdinal, DeviceType.Hardware);
            foreach (string flagsCondition in flagDescriptions.Keys)
            {
                if (SupporesCappability(flagsCondition, caps))
                {
                    string flagsComponent = (string)flagDescriptions[flagsCondition];
                    CreateFlags flag = ParseCreateFlag(flagsComponent.Trim());
                    return flag;
                }
            }
            return flags;
        }

        private bool SupporesCappability(string condition, Caps caps)
        {
            bool supports = false;
            new Dictionary<string, Action>() {
                {"SupportsAdaptiveTessellateNPatch", ()=>{supports = caps.DeviceCaps.SupportsAdaptiveTessellateNPatch; } },
                {"SupportsAdaptiveTessellateRtPatch", ()=>{supports = caps.DeviceCaps.SupportsAdaptiveTessellateRtPatch; } },
                {"SupportsDMapNPatch", ()=>{supports = caps.DeviceCaps.SupportsDMapNPatch; } },
                {"SupportsDrawPrimitives2", ()=>{supports = caps.DeviceCaps.SupportsDrawPrimitives2; } },
                {"SupportsDrawPrimitives2Ex", ()=>{supports = caps.DeviceCaps.SupportsDrawPrimitives2Ex; } },
                {"SupportsDrawPrimitivesTransformedVertex", ()=>{supports = caps.DeviceCaps.SupportsDrawPrimitivesTransformedVertex; } },
                {"SupportsExecuteSystemMemory", ()=>{supports = caps.DeviceCaps.SupportsExecuteSystemMemory; } },
                {"SupportsExecuteVideoMemory", ()=>{supports = caps.DeviceCaps.SupportsExecuteVideoMemory; } },
                {"SupportsHardwareRasterization", ()=>{supports = caps.DeviceCaps.SupportsHardwareRasterization; } },
                {"SupportsHardwareTransformAndLight", ()=>{supports = caps.DeviceCaps.SupportsHardwareTransformAndLight; } },
                {"SupportsNPatches", ()=>{supports = caps.DeviceCaps.SupportsNPatches; } },
                {"SupportsPreSampledDMapNPatch", ()=>{supports = caps.DeviceCaps.SupportsPreSampledDMapNPatch; } },
                {"SupportsPureDevice", ()=>{supports = caps.DeviceCaps.SupportsPureDevice; } },
                {"SupportsQuinticRtPatches", ()=>{supports = caps.DeviceCaps.SupportsQuinticRtPatches; } },
                {"SupportsRtPatches", ()=>{supports = caps.DeviceCaps.SupportsRtPatches; } },
                {"SupportsRtPatchHandleZero", ()=>{supports = caps.DeviceCaps.SupportsRtPatchHandleZero; } },
                {"SupportsSeparateTextureMemories", ()=>{supports = caps.DeviceCaps.SupportsSeparateTextureMemories; } },
                {"SupportsStreamOffset", ()=>{supports = caps.DeviceCaps.SupportsStreamOffset; } },
                {"SupportsTextureNonLocalVideoMemory", ()=>{supports = caps.DeviceCaps.SupportsTextureNonLocalVideoMemory; } },
                {"SupportsTextureSystemMemory", ()=>{supports = caps.DeviceCaps.SupportsTextureSystemMemory; } },
                {"SupportsTextureVideoMemory", ()=>{supports = caps.DeviceCaps.SupportsTextureVideoMemory; } },
                {"SupportsTransformedVertexSystemMemory", ()=>{supports = caps.DeviceCaps.SupportsTransformedVertexSystemMemory; } },
                {"SupportsTransformedVertexVideoMemory", ()=>{supports = caps.DeviceCaps.SupportsTransformedVertexVideoMemory; } }

            }[condition]();
            return supports;
        }

        private CreateFlags ParseCreateFlag(string flag)
        {
            return new Dictionary<string, CreateFlags>
            {
                {"SoftwareVertexProcessing", CreateFlags.SoftwareVertexProcessing },
                {"PureDevice", CreateFlags.PureDevice },
                {"NoWindowChanges", CreateFlags.NoWindowChanges },
                {"MultiThreaded", CreateFlags.MultiThreaded },
                {"MixedVertexProcessing", CreateFlags.MixedVertexProcessing },
                {"HardwareVertexProcessing", CreateFlags.HardwareVertexProcessing },
                {"FpuPreserve", CreateFlags.FpuPreserve },
                {"DisableDriverManagementEx", CreateFlags.DisableDriverManagementEx },
                {"DisableDriverManagement", CreateFlags.DisableDriverManagement },
                {"AdapterGroupDevice", CreateFlags.AdapterGroupDevice },
            }[flag];
        }

        private PresentParameters ParsePresentParams()
        { 
            Dictionary<string, object> parametersMap = (Dictionary<string, object>)configs["PresentParameters"];
            PresentParameters parameters = new PresentParameters();
            int i = 0;
            foreach (string key in parametersMap.Keys)
            {
                ParsePresentParams(parameters, key, (string)parametersMap[key]);
            }

            return parameters;
        }

        private void ParsePresentParams(PresentParameters parameters, string key, string value)
        {
            new Dictionary<string, Action>
            {
                {"AutoDepthStencilFormat", () => { parameters.AutoDepthStencilFormat = ParseDepthFormat(value); } },
                {"BackBufferCount", () => { parameters.BackBufferCount = int.Parse(value); } },
                {"BackBufferFormat", () => { parameters.BackBufferFormat = ParseFormat(value); } },
                {"BackBufferHeight", () => { parameters.BackBufferHeight = int.Parse(value); } },
                {"BackBufferWidth", () => { parameters.BackBufferWidth = int.Parse(value); } },
                {"DeviceWindow", () => { parameters.DeviceWindow = (Control)DI.Get(value); } },
                {"DeviceWindowHandle", () => { parameters.DeviceWindowHandle = ((Control)DI.Get(value)).Handle; } },
                {"EnableAutoDepthStencil", () => { parameters.EnableAutoDepthStencil = bool.Parse(value); } },
                {"ForceNoMultiThreadedFlag", () => { parameters.ForceNoMultiThreadedFlag = bool.Parse(value); } },
                {"FullScreenRefreshRateInHz", () => { parameters.FullScreenRefreshRateInHz = int.Parse(value); } },
                {"MultiSample", () => { parameters.MultiSample = ParseMultiSample(value); } },
                {"MultiSampleQuality", () => { parameters.MultiSampleQuality = int.Parse(value); } },
                {"PresentationInterval", () => { parameters.PresentationInterval = ParsePresentationInterval(value); } },
                {"PresentFlag", () => { parameters.PresentFlag = ParsePresentFlag(value); } },
                {"SwapEffect", () => { parameters.SwapEffect = ParseSwapEffect(value); } },
                {"Windowed", () => { parameters.Windowed = bool.Parse(value); } },
            }[key]();
        }

        DepthFormat ParseDepthFormat(string value)
        {
            return new Dictionary<string, DepthFormat>
            {
                {"D15S1", DepthFormat.D15S1 },
                {"D16", DepthFormat.D16 },
                {"D16Lockable", DepthFormat.D16Lockable },
                {"D24S8", DepthFormat.D24S8 },
                {"D24SingleS8", DepthFormat.D24SingleS8 },
                {"D24X4S4", DepthFormat.D24X4S4 },
                {"D24X8", DepthFormat.D24X8 },
                {"D32", DepthFormat.D32 },
                {"D32SingleLockable", DepthFormat.D32SingleLockable },
                {"L16", DepthFormat.L16 },
                {"Unknown", DepthFormat.Unknown },
            }[value];
        }

        Format ParseFormat(string value)
        {
            return new Dictionary<string, Format>
            {
                {"A16B16G16R16", Format.A16B16G16R16 },
                {"A16B16G16R16F", Format.A16B16G16R16F },
                {"A1R5G5B5", Format.A1R5G5B5 },
                {"A2B10G10R10", Format.A2B10G10R10 },
                {"A2R10G10B10", Format.A2R10G10B10 },
                {"A2W10V10U10", Format.A2W10V10U10 },
                {"A32B32G32R32F", Format.A32B32G32R32F },
                {"A4L4", Format.A4L4 },
                {"A4R4G4B4", Format.A4R4G4B4 },
                {"A8", Format.A8 },
                {"A8B8G8R8", Format.A8B8G8R8 },
                {"A8L8", Format.A8L8 },
                {"A8P8", Format.A8P8 },
                {"A8R3G3B2", Format.A8R3G3B2 },
                {"A8R8G8B8", Format.A8R8G8B8 },
                {"CxV8U8", Format.CxV8U8 },
                {"D15S1", Format.D15S1 },
                {"D16", Format.D16 },
                {"D16Lockable", Format.D16Lockable },
                {"D24S8", Format.D24S8 },
                {"D24SingleS8", Format.D24SingleS8 },
                {"D24X4S4", Format.D24X4S4 },
                {"D24X8", Format.D24X8 },
                {"D32", Format.D32 },
                {"D32SingleLockable", Format.D32SingleLockable },
                {"Dxt1", Format.Dxt1 },
                {"Dxt2", Format.Dxt2 },
                {"Dxt3", Format.Dxt3 },
                {"Dxt4", Format.Dxt4 },
                {"Dxt5", Format.Dxt5 },
                {"G16R16", Format.G16R16 },
                {"G16R16F", Format.G16R16F },
                {"G32R32F", Format.G32R32F },
                {"G8R8G8B8", Format.G8R8G8B8 },
                {"L16", Format.L16 },
                {"L6V5U5", Format.L6V5U5 },
                {"L8", Format.L8 },
                {"Multi2Argb8", Format.Multi2Argb8 },
                {"P8", Format.P8 },
                {"Q16W16V16U16", Format.Q16W16V16U16 },
                {"Q8W8V8U8", Format.Q8W8V8U8 },
                {"R16F", Format.R16F },
                {"R32F", Format.R32F },
                {"R3G3B2", Format.R3G3B2 },
                {"R5G6B5", Format.R5G6B5 },
                {"R8G8B8", Format.R8G8B8 },
                {"R8G8B8G8", Format.R8G8B8G8 },
                {"Unknown", Format.Unknown },
                {"Uyvy", Format.Uyvy },
                {"V16U16", Format.V16U16 },
                {"V8U8", Format.V8U8 },
                {"VertexData", Format.VertexData },
                {"X1R5G5B5", Format.X1R5G5B5 },
                {"X4R4G4B4", Format.X4R4G4B4 },
                {"X8B8G8R8", Format.X8B8G8R8 },
                {"X8L8V8U8", Format.X8L8V8U8 },
                {"X8R8G8B8", Format.X8R8G8B8 },
                {"Yuy2", Format.Yuy2 }
            }[value];
        }

        MultiSampleType ParseMultiSample(string value)
        {
            return new Dictionary<string, MultiSampleType>
            {
                {"EightSamples", MultiSampleType.EightSamples },
                {"ElevenSamples", MultiSampleType.ElevenSamples },
                {"FifteenSamples", MultiSampleType.FifteenSamples },
                {"FiveSamples", MultiSampleType.FiveSamples },
                {"FourSamples", MultiSampleType.FourSamples },
                {"FourteenSamples", MultiSampleType.FourteenSamples },
                {"NineSamples", MultiSampleType.NineSamples },
                {"None", MultiSampleType.None },
                {"NonMaskable", MultiSampleType.NonMaskable },
                {"SevenSamples", MultiSampleType.SevenSamples },
                {"SixSamples", MultiSampleType.SixSamples },
                {"SixteenSamples", MultiSampleType.SixteenSamples },
                {"TenSamples", MultiSampleType.TenSamples },
                {"ThirteenSamples", MultiSampleType.ThirteenSamples },
                {"ThreeSamples", MultiSampleType.ThreeSamples },
                {"TwelveSamples", MultiSampleType.TwelveSamples },
                {"TwoSamples", MultiSampleType.TwoSamples }
            }[value];
        }

        PresentInterval ParsePresentationInterval(string value)
        {
            return new Dictionary<string, PresentInterval>{
                {"Default", PresentInterval.Default },
                {"Four", PresentInterval.Four },
                {"Immediate", PresentInterval.Immediate },
                {"One", PresentInterval.One },
                {"Three", PresentInterval.Three },
                {"Two", PresentInterval.Two },
            }[value];
        }

        PresentFlag ParsePresentFlags(string value)
        {
            PresentFlag flag = 0;
            string[] flagStrings = value.Split(',');
            foreach(string flagString in flagStrings)
            {
                flag |= ParsePresentFlag(flagString.Trim());
            }
            return flag;
        }

        PresentFlag ParsePresentFlag(string value)
        {
            return new Dictionary<string, PresentFlag>
            {
                {"DeviceClip", PresentFlag.DeviceClip },
                {"DiscardDepthStencil", PresentFlag.DiscardDepthStencil },
                {"LockableBackBuffer", PresentFlag.LockableBackBuffer },
                {"None", PresentFlag.None },
                {"Video", PresentFlag.Video }
            }[value];
        }

        SwapEffect ParseSwapEffect(string value)
        {
            return new Dictionary<string, SwapEffect>
            {
                {"Copy", SwapEffect.Copy },
                {"Discard", SwapEffect.Discard },
                {"Flip", SwapEffect.Flip },
            }[value];
        }
    }
}
