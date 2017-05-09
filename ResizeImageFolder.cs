using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ResizeImage
{
     public class ResizeImageFolder {
     private int Wifwidthgreater,Hifwidthgreater; //define as dimensoes caso width > Height
     private int Wifheightgreater,Hifheightgreater; //caso seja height > width
        
 
	 private Bitmap ResizeImage(Image imagem, int w, int h)
     {
             Rectangle dstRect/*=new Rectangle(0,0,w,h)*/;
             Bitmap dstImagem=null;

             
             if(imagem.Width > imagem.Height)
			 {
                  dstImagem=new Bitmap(Wifwidthgreater,Hifwidthgreater);
				  dstRect=new Rectangle(0,0,Wifwidthgreater,Hifwidthgreater);				  
			 }
             else
			 {
			      dstImagem=new Bitmap(Wifheightgreater,Hifheightgreater);
                  dstRect=new Rectangle(0,0,Wifheightgreater,Hifheightgreater);
             }    
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
			 ResizeImageFolder res=new ResizeImageFolder();
             if(args.Length < 6)
	         {
                 Console.WriteLine("Necessário seis parametros ! \n\nResizeImageFolder - Redimensiona imagens de uma pasta dada sua extensão com base no comprimento ou altura\n");
                 Console.WriteLine("ResizeImageFolder [pasta] [extensao sem ponto] [Wsewmaior] [Hsewmaior] [Wsehmaior] [Hsehmaior]");
				 Console.WriteLine("\nOnde:\n");
				 Console.WriteLine("\t [pasta] = pasta destino do resize\n"+
								   "\t [extensao sem ponto] = extensão do arquivo jpg,png,gif\n"+
								   "\t [Wsewmaior] = Width (comprimento) caso a dimensão maior da imagem seja o comprimento\n"+
								   "\t [Hsewmaior] = Heigth (altura) caso a dimensão maior da imagem seja o comprimento\n"+
								   "\t [Wsehmaior] = Width (comprimento) caso a dimensão maior da imagem seja a altura (height)\n"+
								   "\t [Hsehmaior] = Heigth (altura) caso a dimensão maior da imagem seja a altura(height)\n");				 
             }
             else
             {
                try
                {
                    //W>H
                    res.Wifwidthgreater=int.Parse(args[2]);
                    res.Hifwidthgreater=int.Parse(args[3]);

                    //H>W
                    res.Wifheightgreater=int.Parse(args[4]);
                    res.Hifheightgreater=int.Parse(args[5]);

                    if(Directory.Exists(args[0]))
		            {
                        string []lista=Directory.GetFiles(args[0],"*."+args[1]);

		   	            foreach(string s in lista)
	                    {
                            Bitmap mapa=res.ResizeImage( (Bitmap)Image.FromFile(s,true), 500, 400 );
                            mapa.Save(Path.GetDirectoryName(s)+"\\r"+Path.GetFileName(s));
                        }  

                    }
                    else
                       Console.WriteLine("Diretorio nao existe");
	            }
                catch(Exception e)
                {
                    Console.WriteLine("Um erro ocorreu durante a execução do programa: "+e.Message);
                }
            
         }

     }

}
}
