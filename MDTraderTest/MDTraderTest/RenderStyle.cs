/*****************************************************************************\
 *                                                                           *
 *                    Unpublished Work Copyright (c) 2006                    *
 *                  Trading Technologies International, Inc.                 *
 *                       All Rights Reserved Worldwide                       *
 *                                                                           *
 *          * * *   S T R I C T L Y   P R O P R I E T A R Y   * * *          *
 *                                                                           *
 * WARNING:  This program (or document) is unpublished, proprietary property *
 * of Trading Technologies International, Inc. and  is  to be  maintained in *
 * strict confidence. Unauthorized reproduction,  distribution or disclosure *
 * of this program (or document),  or any program (or document) derived from *
 * it is  prohibited by  State and Federal law, and by local law  outside of *
 * the U.S.                                                                  *
 *                                                                           *
 *****************************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;

using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Tools;

namespace TT.SP.Trading.Controls.MDTrader
{
    /// <summary>
    /// Color information for rendering MDTrader controls
    /// </summary>
    public class RenderStyle
    {
        #region Private Data
        private System.Drawing.Color _back;
        private System.Drawing.Color _fore;
        private Syncfusion.Drawing.BrushInfo _bgBrush;
        #endregion

        #region CTOR
        public RenderStyle(Color back, Color fore, BrushInfo bgBrush)
        {
            _back = back;
            _fore = fore;
            _bgBrush = bgBrush;
        }
        public RenderStyle(BrushInfo bgBrush)
        {
            _back = System.Drawing.Color.White;
            _fore = System.Drawing.Color.Black;
            _bgBrush = bgBrush;
        }
        #endregion

        #region Public Methods
        public void Apply(Control ctrl)
        {
            ctrl.BackColor = _back;
            ctrl.ForeColor = _fore;
        }
        public void Apply(GradientLabel gl)
        {
            Apply((Control)gl);
            if (_bgBrush != null)
                gl.BackgroundColor = _bgBrush;
        }
        public void Apply(GradientPanel gp)
        {
            Apply((Control)gp);
            if (_bgBrush != null)
                gp.BackgroundColor = _bgBrush;
        }
        #endregion Public Methods

        #region Public Static Settings
        //
        // Net position specific colors
        //
        public static RenderStyle NetposFlat = new RenderStyle(
            Color.White,
            Color.Black,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                Color.LightGray,
                Color.White)
        );
        public static RenderStyle NetposFlatMono = new RenderStyle(
            Color.White,
            Color.Gray,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                Color.LightGray,
                Color.White)
        );
        public static RenderStyle NetposShort = new RenderStyle(
            Color.Red,
            Color.White,
            new BrushInfo(GradientStyle.BackwardDiagonal, new BrushInfoColorArrayList(new Color[]{ 
                Color.Red, 
                Color.DarkRed 
            }))
        );
        public static RenderStyle NetposShortMono = new RenderStyle(
            Color.FromArgb(255, 200, 200),
            Color.White,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                Color.FromArgb(255, 200, 200),
                Color.FromArgb(255, 127, 127))
        );
        public static RenderStyle NetposLong = new RenderStyle(
            Color.LightBlue,
            Color.White,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                Color.LightBlue,
                Color.DarkBlue)
        );
        public static RenderStyle NetposLongMono = new RenderStyle(
            Color.FromArgb(200, 200, 255),
            Color.White,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                Color.FromArgb(200, 200, 255),
                Color.FromArgb(127, 127, 255))
        );

        // Generic Mono Settings
        public static RenderStyle Mono = new RenderStyle(
            Color.FromArgb(192, 192, 192),
            Color.Gray,
            new BrushInfo(GradientStyle.ForwardDiagonal,
                Color.FromArgb(192, 192, 192),
                System.Drawing.Color.White)
        );

        // Header control
        public static RenderStyle HeaderPanel = new RenderStyle(
            SystemColors.ActiveCaption,
            Color.White,
            new BrushInfo(GradientStyle.BackwardDiagonal,
                SystemColors.InactiveCaption,
                SystemColors.ActiveCaption)
        );

        // Subheader controls
        public static RenderStyle SubHeader = new RenderStyle(
            SystemColors.ActiveCaption,
            Color.White,
            new BrushInfo(GradientStyle.Horizontal,
                SystemColors.Highlight,
                SystemColors.HotTrack)
        );
        #endregion // Public Static Settings
    };

    /// <summary>
    /// Enumeration for the different render types of the MDTrader control.
    /// </summary>
    public enum RenderMode
    {
        Color,
        Mono
    }
}
