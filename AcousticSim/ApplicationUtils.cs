/*  Wavelet Studio Signal Processing Library - www.AcousticSim.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using Envision.Blocks;
using Envision;
using DiagramNet.Elements;
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Envision.Designer.Utils
{
    public static class ApplicationUtils
    {

       

        public static void SetAssociation(string Extension,  string OpenWith, string FileDescription)
        {
            // The stuff that was above here is basically the same

            // Delete the key instead of trying to change it
            var CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension, true);
            CurrentUser.DeleteSubKey("UserChoice", false);
            CurrentUser.Close();

            // Tell explorer the file association has been changed
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
 
        
        public static DiagramBlock CreateDiagramBlock(BlockBase block, bool useNextPosition)
        {
            var className = (block.ID == null) ?  block.GetAssemblyClassName() : block.ID;
            Image image = Envision.Properties.Resources.unknownicon;
            if (block.Picture != null)
            {
                image = block.Picture;
            }
            var label = className;
            var shortname = typeof(BlockOutputNode).GetProperty("ShortName");
            return new DiagramBlock(image, label, block, block.InputNodes.ToArray<object>(), block.OutputNodes.ToArray<object>(), shortname, true, useNextPosition);
        }

        public static BlockBase AsBlock(object item)
        {
            var elem = (DiagramNet.Elements.DiagramBlock)item;
            BlockBase block = (BlockBase)elem.State;
            return block;
        }

        public static bool IsBlock(object item)
        {
            if ((item.GetType() == typeof(DiagramNet.Elements.RightAngleLinkElement))
                    || (item.GetType() == typeof(DiagramNet.Elements.StraightLinkElement))
                        || (item.GetType() == typeof(DiagramNet.Elements.ConnectorElement))
                          || (item.GetType() == typeof(DiagramNet.Elements.LabelElement))
                           || (item.GetType() == typeof(DiagramNet.Elements.CommentBoxElement)))
            {
                return false;
            }
            return true;
        }


       



        public static Image ResizeTo(this Image image, int width, int height)
        {
            if (image.Width < width && image.Height < height)
            {
                return image;
            }
            var bitmap = new Bitmap(width, height);
            var graph = Graphics.FromImage(bitmap);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.DrawImage(image, 0, 0, width, height);
            graph.Dispose();
            return bitmap;
        }


        public static string RemoveSpecialChars(this string text)
        {
            const string past = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";
            const string future = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";
            const string not = "()@$%?#\"'\\/:<>|*-+";
            for (var i = 0; i < past.Length; i++)
            {
                text = text.Replace(past[i].ToString(CultureInfo.InvariantCulture), future[i].ToString(CultureInfo.InvariantCulture));
            }
            foreach (var t in not)
            {
                text = text.Replace(t.ToString(CultureInfo.InvariantCulture), "");
            }
            return text;
        }


        public static bool IsConnectedAtAll(BlockBase block)
        {
            foreach (var item in block.InputNodes)
            {
                if (item.ConnectingNode != null) return true;
            }
            foreach (var item in block.OutputNodes)
            {
                if (item.ConnectingNode != null) return true;                
            }
            return false;
        }
  
    }
}
