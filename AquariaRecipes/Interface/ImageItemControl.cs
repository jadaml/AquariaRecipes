﻿/* Copyright (c) 2018, 2020, Ádám L. Juhász
 *
 * This file is part of AquariaRecepies.
 *
 * AquariaRecepies is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * AquariaRecepies is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with AquariaRecepies.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAL.AquariaRecipes.Interface
{
    public partial class ImageItemControl : UserControl
    {
        private Bitmap m_ws;

        new public string Text
        {
            get => lblText.Text;
            set => lblText.Text = value;
        }

        protected override Size DefaultSize => new Size(150, 28);

        public Image Image
        {
            get => picImage.Image;
            set => picImage.Image = value;
        }

        public int ImageWidth
        {
            get => picImage.Width;
            set => picImage.Width = value;
        }

        public ImageItemControl()
        {
            InitializeComponent();
        }

        public void MeasureItem(MeasureItemEventArgs e, string text, Image image)
        {
            AutoSize = true;
            Text     = text;
            Image    = image;

            e.ItemWidth  = PreferredSize.Width;
            e.ItemHeight = PreferredSize.Height;
        }

        public void DrawItem(DrawItemEventArgs e, string text, Image image)
        {
            AutoSize  = false;
            Text      = text;
            Image     = image;
            Font      = e.Font;
            BackColor = e.BackColor;
            ForeColor = e.ForeColor;
            Size      = e.Bounds.Size;

            ResizeWorkspace(e.Bounds.Size, e.Graphics);
            DrawToBitmap(m_ws, new Rectangle(Point.Empty, Size));
            //m_ws.Save($"Item_{e.Index}_{e.Bounds.Width}_{e.Bounds.Height}.png");

            e.DrawBackground();
            e.Graphics.DrawImage(m_ws, e.Bounds.Location);
            e.DrawFocusRectangle();
        }

        private void ResizeWorkspace(Size size, Graphics g)
        {
            int w, h;

            for (w = m_ws == null ? 16 : m_ws.Width; w < size.Width; w *= 2) ;
            for (h = m_ws == null ? 16 : m_ws.Height; h < size.Height; h *= 2) ;

            if (m_ws == null
             || w != m_ws.Width
             || h != m_ws.Height)
            {
                m_ws = new Bitmap(w, h, g);
            }
        }
    }
}
