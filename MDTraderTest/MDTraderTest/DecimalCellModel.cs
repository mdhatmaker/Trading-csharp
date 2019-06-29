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

using Syncfusion.Windows.Forms.Grid;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TT.SP.Trading.Controls.MDTrader
{
    /// <summary>
    /// eborts 2006/03/01
    /// 
    /// DESCRIPTION
    /// Cell model which aligns text to decimal point
    /// Grid "cell models" are used for serialization.
    /// Grid "render models" are used for drawing.
    /// 
    /// FEATURES
    /// - Align text to decimal point. 
    /// - Decimal point is right-aligned to a given number of decimal places
    ///     ex. N = 3
    ///                 |----| display column
    /// 
    ///                 |____|  Cell Text
    ///                1|.1  |  1.1
    ///               10|.0  |  10.0
    ///                0|.001|  0.001
    ///              100|.0  |  100.0
    ///                 |----|          
    /// 
    /// - Automatically shifts to show the least significant digits (trader mode)
    ///   if column is too narrow.
    ///     ex. N = 2
    ///                 |---| display column
    /// 
    ///                 |___|   Cell Text      
    ///             1000|.1 |   1000.1      
    ///             1000|.05|   1000.05
    ///            1000.|025|   1000.025
    ///           1000.0|125|   1000.0125
    ///                 |---|          
    /// 
    /// - Obeys all cell formatting rules if there is no decimal point. 
    /// 
    /// BUGS
    /// - 1 or 2 pixel error, possibly due to inaccuracies in MeasureString, such 
    ///   as passing of string to contain character glyphs.
    /// 
    /// </summary>
    /// <example>
    /// class Form1
    /// {
    ///     GridControl _gridControl1;
    ///     DecimalCellModel _cellModel;
    /// 
    ///     void InitCells()
    ///     {
    ///         // register the cell model with a grid control
    ///         _cellModel = DecimalCellModel.Register(_gridControl1);
    /// 
    ///         // control the number of places after the decimal to align
    ///         _cellModel.PlacesAfterDecimal = 3;
    /// 
    ///         // add an additional margin of 5 pixels on the right side
    ///         _cellModel.RightMargin = 5.0f; 
    ///     
    ///         // change the cell type
    ///         _gridControl1[1, 1].CellType = DecimalCellModel.CellType;
    /// 
    ///         // Set the text to "1.2" which will appear as "1.2  "
    ///         // because PlacesAfterDecimal is set to 3
    ///         _gridControl1[1, 1].Text = "1.2";
    ///     }
    /// }
    /// 
    /// [Example2]
    /// // Use decimal alignment for cells with decimal points '.'.
    /// // Otherwise use centered alignment.
    /// void InitCells()
    /// {
    ///     _gridControl1[1,1].CellType = DecimalCellModel.CellType;
    ///     _gridControl1[1,1].HorizontalAlignment = GridHorizontalAlignment.Center;
    ///     _gridControl1[1,1].Text = "1.0";    // decimal aligned
    ///     _gridControl1[2,1].CellType = DecimalCellModel.CellType;
    ///     _gridControl1[2,1].HorizontalAlignment = GridHorizontalAlignment.Center
    ///     _gridControl1[2,1].Text = "1 : 0";  // center aligned
    /// }
    /// 
    /// </example>
    ///
    [Serializable]
    public class DecimalCellModel : GridStaticCellModel
    {
        #region Static Helper Methods
        /// <summary>
        /// Register the cell model with a grid control.
        /// </summary>
        /// <returns>The model instance registered to the grid control</returns>
        /// <remarks>Uses <c>CellType</c> when registering.</remarks>
        public static DecimalCellModel Register(GridControl gc, char padChar)
        {
            if (gc == null)
                throw new ArgumentNullException("gc");

            DecimalCellModel cm = new DecimalCellModel(gc.Model, padChar);
            gc.CellModels.Add(CellType, cm);
            return cm;
        }
        /// <summary>
        /// String name for registration and cell type customization
        /// </summary>
        /// <example>
        /// _gridControl1.CellType = DecimalCellModel.CellType
        /// </example>
        public static string CellType
        {
            get { return "DecimalCellModel"; }
        }
        #endregion

        #region CTOR
        /// <summary>
        /// Required. For creation within a form.
        /// Called from <c>DecimalCellModel.Register</c>
        /// </summary>
        public DecimalCellModel(GridModel grid, char padChar)
            : base(grid)
        {
            this._padChar = padChar;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Model creates Renderer
        /// </summary>
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new DecimalCellModel.Renderer(control, this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of places after the decimal point to display for
        /// string alignment. 
        /// Setting this to a negative value will turn OFF alignment.
        /// </summary>
        public int PlacesAfterDecimal
        {
            set{ _placesAfterDecimal = value; }
            get{ return _placesAfterDecimal; }

        } private int _placesAfterDecimal;

        /// <summary>
        /// Width of right aligned padding (in display units (pixels?))
        /// </summary>
        public float RightMargin
        {
            set { _rightPadding = value; }
            get { return _rightPadding; }

        } private float _rightPadding;

        /// <summary>
        /// Character base aligning to #places after the decimal point
        /// </summary>
        private char _padChar = '0';
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        protected DecimalCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        /// <summary>
        /// Render text with decimal place alignment
        /// </summary>
        internal class Renderer : GridStaticCellRenderer
        {
            #region Private Members
            /// <summary>
            /// Alignment in "graphics units" from right side of render area
            /// to the left side of decimal point
            /// </summary>
            float _alignment; 
            /// <summary>
            /// Number of places after the decimal point. 
            /// Cached from allocating DecimalCellModel. If the model changes,
            /// the renderer will recalculate <c>_alignment</c>
            /// </summary>
            int _placesAfterDecimal;
            /// <summary>
            /// Typed reference to the allocating cell model. Allows us to
            /// detect dirty condition for the renderer (e.g. <c>_placesAfterDecimal</c>
            /// </summary>
            DecimalCellModel _cellModel;
            #endregion

            #region CTOR
            public Renderer(GridControlBase grid, DecimalCellModel cellModel)
                : base(grid, cellModel)
            {
                this._cellModel = cellModel;
            }
            #endregion

            #region Overrides
            protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
            {
                float lhs = 0.0f;
                int decimalPos = -1;

                // only recalculate the right hand side width if the cached decimal points have changed
                if (_placesAfterDecimal != _cellModel._placesAfterDecimal)
                {
                    // places has changed
                    _placesAfterDecimal = _cellModel._placesAfterDecimal;
                    if (_placesAfterDecimal >= 0)
                    {
                        // Include the decimal point when rendering for alignment purposes
                        string sPadding = "." + new String(_cellModel._padChar, _placesAfterDecimal);
                        float width = g.MeasureString(sPadding, style.Font.GdipFont, PointF.Empty, StringFormat.GenericTypographic).Width;
                        width = (float)Math.Ceiling(width);
                        _alignment = Math.Max(0.0f, width);
                    }
                }

                // only modify the style if the desired places after decimal is positive
                if (_placesAfterDecimal >= 0)
                {
                    // find the decimal point
                    string sText = style.Text;
                    decimalPos = sText.IndexOf('.');
                    if (decimalPos >= 0)
                    {
                        // Measure the string up to (but not including) the decimal point.
                        lhs = g.MeasureString(sText.Substring(0, decimalPos), style.Font.GdipFont, PointF.Empty, StringFormat.GenericTypographic).Width;
                    }
                }

                if (decimalPos < 0) // no decimal point
                    base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
                else
                {
                    // Draw everything but the text
                    string sBackup = style.Text;
                    style.ResetText();
                    base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
                    style.Text = sBackup;

                    // draw text clipped to cell boundaries
                    RectangleF oldClip = g.ClipBounds;
                    g.SetClip(clientRectangle);                     // clip to cell boundaries
                    using (Brush newBrush = new SolidBrush(style.TextColor))
                    {
                        RectangleF rect = base.GetCellClientRectangle(rowIndex, colIndex, 
                            GridStyleInfo.Empty,
                            false);

                        float left = rect.Right - _alignment - lhs - _cellModel._rightPadding;
                        float top = rect.Top + (rect.Height - style.Font.GdipFont.Height) * 0.5f;
                        g.DrawString(style.Text, style.Font.GdipFont, newBrush, new PointF(left, top));
                    }
                    g.SetClip(oldClip);                             // restore clipping
                }
            }
            #endregion
        }
    }

}
