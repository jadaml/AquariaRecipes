﻿/* Copyright (c) 2018, Ádám L. Juhász
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
using JAL.AquariaRecipes.Recipes;

namespace JAL.AquariaRecipes.Interface
{
    public partial class RecipeItem : UserControl
    {
        private Bitmap               m_ws;
        private IngredientCollection recipe;
        private int                  width = 64;

        public IngredientCollection Recipe
        {
            get => recipe;
            set
            {
                recipe = value;

                lblCombine2   .Visible = recipe?.IsComplexRecepie ?? false;
                picIngredient3.Visible = recipe?.IsComplexRecepie ?? false;

                picIngredient1.Image = recipe?.ElementAtOrDefault(0)?.Image;
                picIngredient2.Image = recipe?.ElementAtOrDefault(1)?.Image;
                picIngredient3.Image = recipe?.ElementAtOrDefault(2)?.Image;
            }
        }

        public int ImageWidth
        {
            get => width;
            set
            {
                picIngredient1.Width = value;
                picIngredient2.Width = value;
                picIngredient3.Width = value;
                width                = value;
            }
        }

        public RecipeItem()
        {
            InitializeComponent();
        }

        public void DrawItem(DrawItemEventArgs e)
        {
            AutoSize  = false;
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
