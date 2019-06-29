/*****************************************************************************\
 *                                                                           *
 *                Unpublished Work Copyright (c) 2005 - 2006                 *
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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TT.SP.Trading.Controls.MDTrader
{
	/// <summary>
	/// PerformanceGrid is based on hints and suggestions for a high performance grid that are given in the
	/// Syncfusion documentation.
	/// </summary>
    public class PerformanceGridControl : GridControl
    {
        #region PRIVATE MEMBERS
        private bool useGDI;
        private BufferedGraphicsContext _bufferedGraphicsContext = new BufferedGraphicsContext();
        private BufferedGraphics _bufferGraphics;
        private PaintEventArgs _bufferedPaintEventArgs;
        #endregion

        #region CTORS
        public PerformanceGridControl()
        {
            SetStyle(ControlStyles.DoubleBuffer, false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);

        }
        #endregion

        #region PROPERTIES
        /// 
        /// Property UseGDI (bool)
        /// 
        public bool UseGDI
        {
            [DebuggerStepThrough]
            get { return this.useGDI; }
            [DebuggerStepThrough]
            set
            {
                if (this.UseGDI != value)
                {
                    this.useGDI = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region PROTECTED METHODS
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_bufferedPaintEventArgs != null)
            {
                _bufferedPaintEventArgs.Dispose();
                _bufferedPaintEventArgs = null;
            }

            if (_bufferGraphics != null)
            {
                Debug.Assert( _bufferedGraphicsContext != null );
                
                _bufferGraphics.Dispose();
                _bufferGraphics = null;
            
                _bufferedGraphicsContext.Dispose();
                _bufferedGraphicsContext = null;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Reset the buffered graphics context and the associated bufferedGraphics so that
            // they have the current size of the window.  This allows us to keep using the same
            // buffer* objects for as long as the size of the window doesn't repaint.
            _bufferedGraphicsContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            if (_bufferGraphics != null)
            {
                _bufferGraphics.Dispose();
                _bufferGraphics = null;
            }

            if (_bufferedPaintEventArgs != null)
            {
                _bufferedPaintEventArgs.Dispose();
                _bufferedPaintEventArgs = null;
            }

            _bufferGraphics = _bufferedGraphicsContext.Allocate(
                                 this.CreateGraphics(),
                                 new Rectangle(0, 0, this.Width+1, this.Height+1));


        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //if (useDoubleBuffer)
            //  base.OnPaintBackground (pevent);
        }
        [DebuggerStepThrough]
        protected override void OnPaint(PaintEventArgs e)
        {
            // The _bufferGraphics object should be initialized in the OnResize code BEFORE we ever
            // see our first paint event.  If you are hitting this Assert, then that assumption
            // has proven to be untrue.
            Debug.Assert(_bufferGraphics != null);

            // The code below checks to see if the paint event arguments in use this time are the 
            // same as the ones last time.  Specifically we care about the ClipRectangle, and if it's
            // changed since the last paint.  If it hasn't changed ( which seems to be about 20% of the time )
            // then there is no reason to hit the heap for a new instance of the PaintEventArgs.
            if (_bufferedPaintEventArgs == null || _bufferedPaintEventArgs.ClipRectangle != e.ClipRectangle)
            {
                if (_bufferedPaintEventArgs != null)
                    _bufferedPaintEventArgs.Dispose();

                _bufferedPaintEventArgs = new PaintEventArgs(_bufferGraphics.Graphics, e.ClipRectangle);
            }

            // Make sure the clip is consistent
            _bufferedPaintEventArgs.Graphics.Clip = e.Graphics.Clip;

            // Paint into the offscreen memory buffer, 1st.
            // first render the base chart control, and this level's paint
            // Rendering is done into the _bufferedGraphics, since that's the Graphics
            // that is indicated in the PaintEventsArg that is being passed in.
            base.OnPaint(_bufferedPaintEventArgs);            

            // Copy/render to the graphics object provided by the OnPaint.  Think of this as a
            // BitBlt operation -- the contents of the offscreen buffered graphics are going to
            // be transfered directly to e.Graphics.  This prevents the heavy soft-page faulting
            // because we aren't having the GDI try to guess when it should do the paging,
            // and we're not dropping the off-screen memory buffer in between paint calls.
            _bufferGraphics.Render(e.Graphics);            
        }
        protected override void OnDrawCellDisplayText(GridDrawCellDisplayTextEventArgs e)
        {
            base.OnDrawCellDisplayText (e);

            if (!useGDI || e.Cancel)
                return;

            e.Cancel = GridGdiPaint.Instance.DrawText(e.Graphics, e.DisplayText, e.TextRectangle, e.Style);
        }
        protected override void OnFillRectangleHook(GridFillRectangleHookEventArgs e)
        {
            base.OnFillRectangleHook (e);

            if (!useGDI || e.Cancel)
                return;

            e.Cancel = GridGdiPaint.Instance.FillRectangle(e.Graphics, e.Bounds, e.Brush);
        }
        #endregion
    }
}
