using Android;
using Android.Util;
using Android.Views;
using Android.Content;
using Android.Graphics;

using Color = Android.Graphics.Color;
using Paint = Android.Graphics.Paint;
using Path = Android.Graphics.Path;
using View = Android.Views.View;

namespace HandlersApp.Controls
{
    public class DrawViewAndroid : View
    {
        //drawing path
        private Path drawPath;

        //drawing and canvas paint
        private Paint drawPaint, canvasPaint;

        public Color PaintColor
        {
            get { return drawPaint?.Color ?? Color.Black; }
            set { drawPaint.Color = value; }
        }

        //canvas
        private Canvas drawCanvas;

        //canvas bitmap
        private Bitmap canvasBitmap;

        public DrawViewAndroid(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize();
        }

        public DrawViewAndroid(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public DrawViewAndroid(Context context) : base(context)
        {
            Initialize();
        }

        void Initialize()
        {
            drawPath = new Path();
            drawPaint = new Paint();
            drawPaint.Color = Color.Black;
            drawPaint.AntiAlias = true;
            drawPaint.StrokeWidth = 20;
            drawPaint.SetStyle(Paint.Style.Stroke);
            drawPaint.StrokeJoin = Paint.Join.Round;
            drawPaint.StrokeCap = Paint.Cap.Round;
            canvasPaint = new Paint(PaintFlags.Dither);
            DrawingCacheEnabled = true;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            canvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
        }

        protected override void OnDraw(Canvas canvas)
        {
            drawCanvas = canvas;
            canvas.DrawBitmap(canvasBitmap, 0, 0, canvasPaint);
            canvas.DrawPath(drawPath, drawPaint);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var touchX = e.GetX();
            var touchY = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    drawPath.MoveTo(touchX, touchY);
                    break;
                case MotionEventActions.Move:
                    drawPath.LineTo(touchX, touchY);
                    break;
                default:
                    return false;
            }
            Invalidate();
            return true;
        }

        public void Clear()
        {
            var color = PaintColor;
            Initialize();
            Invalidate();
            PaintColor = color;
        }

        public void Save()
        {
            if (MainActivity.Instance.CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Granted)
            {
                try
                {
                    var path = Android.OS.Environment.GetExternalStoragePublicDirectory(
                        Android.OS.Environment.DirectoryPictures);
                    var filename = System.IO.Path.Combine(path.ToString(), $"{Guid.NewGuid()}.png");

                    var bitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
                    var canvas = new Canvas(bitmap);
                    this.Draw(canvas);

                    var stream = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                    var byteArray = stream.ToArray();

                    System.IO.File.WriteAllBytes(filename, byteArray);

                    var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                    mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filename)));
                    Android.App.Application.Context.SendBroadcast(mediaScanIntent);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                MainActivity.Instance.RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }

        }
    }

}
