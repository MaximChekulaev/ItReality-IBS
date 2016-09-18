using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Camera = Android.Hardware.Camera;
using Android.Content.Res;
using Android.Hardware;

namespace itreality
{
    [Activity(Label = "itreality", MainLauncher = true, Icon = "@drawable/icon"),]
    public class MainActivity : Activity, TextureView.ISurfaceTextureListener, Camera.IAutoFocusCallback
    {
        Camera _camera;
        TextureView _textureView;
       // SurfaceView _preview;
        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int w, int h)
        {
            _camera = Camera.Open();

            _textureView.LayoutParameters =
                   new FrameLayout.LayoutParams(w, h);

            try
            {
                _camera.SetPreviewTexture(surface);
                _camera.StartPreview();

            }
            catch (Java.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (Resources.Configuration.Orientation != Android.Content.Res.Orientation.Landscape)
                _camera.SetDisplayOrientation(90);
            else
                _camera.SetDisplayOrientation(0);        
            _camera.StartPreview();
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview();
            _camera.Release();

            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
           // throw new NotImplementedException();
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            
            //throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            RequestWindowFeature(WindowFeatures.NoTitle);
            _textureView = new TextureView(this);
            _textureView.SurfaceTextureListener = this;
            _textureView.Click += OnDisplayClick;
            SetContentView(_textureView);
        }     
        public void OnDisplayClick(object obj, EventArgs e)
        {
            _camera.AutoFocus(this);
        }
        public void OnAutoFocus(bool success, Camera camera)
        {
            if (success)
            {

                // throw new Exception();
            }
        }
    }
}

