using AVFoundation;
using CoreAnimation;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;


namespace QRCodeScanner
{
    public partial class VCQRCode : UIViewController, IAVCaptureMetadataOutputObjectsDelegate
    {
        //Declare your session
        AVCaptureSession session;
        //Container for the previous Code text
        string str_previous_scanned = "";

        public VCQRCode (IntPtr handle) : base (handle)
        {
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //Ask for permission and initialize the session for camera like a normal property
            var task = authorizeCamera();
            session = new AVCaptureSession();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //Call the function to setup the session for camera
            setupCaptureSession();
        }
        void setupCaptureSession()
        {
            //Create a device for capturing Barcodes
            var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaTypes.Video);
            //Configure the dvice for something fancy autofocus stuffs
            ConfigureCameraForDevice(captureDevice);
            //Create an input from that device - meaning to instaniate the device to make an input node... err something like that
            var captureInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            //Add the input to the session
            session.AddInput(captureInput);

            //Create a preview layer for the view
            var previewLayer = AVCaptureVideoPreviewLayer.FromSession(session);
            previewLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;
            previewLayer.Frame = vie_Preview_cam.Frame;

            //Add the preview layer to the View for the camera uiview
            vie_Preview_cam.Layer.AddSublayer(previewLayer);

            //Assign who's going to handle the metadataoutput
            var metadataoutput = new AVCaptureMetadataOutput();
            //Set delegate
            metadataoutput.SetDelegate(this, CoreFoundation.DispatchQueue.MainQueue);
            //Add the metadataoutput to session
            session.AddOutput(metadataoutput);

            //Assign which type of Codes will be read, 
            metadataoutput.MetadataObjectTypes = AVMetadataObjectType.QRCode;

            //Start the Session
            session.StartRunning();
        }

        // This delegate will catch all the barcodes that has been read
        [Export("captureOutput:didOutputMetadataObjects:fromConnection:")]
        public void DidOutputMetadataObjects(AVCaptureMetadataOutput captureOutput, AVMetadataObject[] metadataObjects, AVCaptureConnection connection)
        {
            foreach (var m in metadataObjects)
            {
                var avmmrcobj_readable = (AVMetadataMachineReadableCodeObject)m;
                if (avmmrcobj_readable.StringValue != str_previous_scanned)
                {
                    str_previous_scanned = avmmrcobj_readable.StringValue;
                    lab_Result_scanned.Text = str_previous_scanned;
                }
            }
        }

        // Ask for permission for camera
        async Task authorizeCamera()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }

        //Configure the camera so it has autofocus and stuffs like autoadjust exposure
        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }
    }
}