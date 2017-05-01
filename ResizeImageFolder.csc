using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ResizeImage
{
     public class ResizeImageFolder {

 
	 public static Bitmap ResizeImage(Image imagem, int w, int h)
         {
             Rectangle dstRect=new Rectangle(0,0,w,h);
             Bitmap dstImagem=new Bitmap(w,h);

             dstImagem.SetResolution(imagem.HorizontalResolution,imagem.VerticalResolution);

             using(var graphics = Graphics.FromImage(dstImagem))
             {
		  graphics.CompositingMode = CompositingMode.SourceCopy;
                  graphics.CompositingQuality = CompositingQuality.HighQuality;
		  graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                  graphics.SmoothingMode =  SmoothingMode.HighQuality;
                  graphics.PixelOffsetMode= PixelOffsetMode.HighQuality;
                  using(var wrapMode = new ImageAttributes())
                  {                      
                     wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                     graphics.DrawImage(imagem,dstRect,0,0,imagem.Width,imagem.Height,GraphicsUnit.Pixel, wrapMode);
                  }
             }

             return dstImagem;     
         }


         public static void Main(string[] args)
         {
             if(args.Length < 2)
                 Console.WriteLine("Necessário dois parametros !");
             else
             {
                if(Directory.Exists(args[0]))
		{
                   string []lista=Directory.GetFiles(args[0],"*."+args[1]);

		   	foreach(string s in lista)
	                {
                            Bitmap mapa=ResizeImage( (Bitmap)Image.FromFile(s,true), 500, 400 );
                            mapa.Save(Path.GetDirectoryName(s)+"\\r"+Path.GetFileName(s));
                        }  

                }
                else
                   Console.WriteLine("Diretorio nao existe");
	     }
            
         }

     }

}
