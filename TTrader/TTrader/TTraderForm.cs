using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TTAPI;

namespace TTrader
{
    public partial class TTraderForm : Form
    {
        private const int LENGTH = 10;

        private TTAPI.Trader tt = new TTAPI.Trader();

        private int _volume, _previousVolume;
        private int _bidAskDelta;
        private int _bidAskDelta1Minute;
        private int _volumeChange;
        private int[] _volumeChanges = new int[LENGTH];
        private int _index;
        private Quote _lastQuote;
        private Quote _price1Minute = new Quote(-1, 0, 0, 0, 0, 0, 0, 0);
        private Queue<int> _bidAskDeltaQ = new Queue<int>();
        private Queue<Quote> _priceQ = new Queue<Quote>();

        public TTraderForm()
        {
            InitializeComponent();

            int quoteID = tt.AddInstrument(new Instrument("eCBOT", "FUTURE", "YM", "MAR06"));

            tt.QuoteUpdate +=new TTAPI.Trader.QuoteUpdateHandler(tt_QuoteUpdate);
        }

        private void tt_QuoteUpdate(object source, Quote quote)
        {
            // Indicate this quote as the last quote so the timer method
            // that stores the price every second can use it.
            _lastQuote = quote;

            _volume = quote.Volume;

            // If the volume has changed, then a trade has occurred.
            if (_volume != _previousVolume)
            {
                _volumeChange += _volume - _previousVolume;

                // If we TRADED ON THE BID...
                if (quote.LastPrice == quote.BidPrice)
                {
                    lblLastPrice.BackColor = Color.Red;
                    _bidAskDelta -= quote.LastQty;
                }
                // If we TRADED ON THE OFFER...
                else if (quote.LastPrice == quote.AskPrice)
                {
                    lblLastPrice.BackColor = Color.Blue;
                    _bidAskDelta += quote.LastQty;
                }
                else
                    lblLastPrice.BackColor = Color.Gray;

                updateRatio();

                _previousVolume = _volume;
            }

            lblBidSize.Text = quote.BidQty.ToString();
            lblBidPrice.Text = quote.BidPrice.ToString();
            lblAskPrice.Text = quote.AskPrice.ToString();
            lblAskSize.Text = quote.AskQty.ToString();

            lblLastPrice.Text = quote.LastPrice.ToString();
            lblLastSize.Text = quote.LastQty.ToString();
        }

        // Every 1/2 second, calculate the VPM (volume-per-minute)
        // based on the average volume for the past 5 seconds.
        private void timer1_Tick(object sender, EventArgs e)
        {
            _volumeChanges[_index] = _volumeChange;
            _volumeChange = 0;

            _index++;
            if (_index >= LENGTH)
                _index = 0;

            double average1 = 0;
            for (int i = 0; i < LENGTH; i++)
            {
                average1 += _volumeChanges[i];
            }
            average1 = (average1 * 60) / LENGTH;

            lblVPM.Text = average1.ToString();
        }

        // Every 1 second, update the change in price and change in bidAskDelta.
        private void timer2_Tick(object sender, EventArgs e)
        {
            const int SECONDS = 15;

            _priceQ.Enqueue(_lastQuote);
            if (_priceQ.Count > SECONDS)
            {
                _price1Minute = _priceQ.Dequeue();
            }
            
            _bidAskDeltaQ.Enqueue(_bidAskDelta);
            if (_bidAskDeltaQ.Count > SECONDS)
            {
                _bidAskDelta1Minute = _bidAskDeltaQ.Dequeue();
            }

            updateRatio();
        }

        private void updateRatio()
        {
            if (_price1Minute.ID == -1)
                return;

            int priceChange = _lastQuote.LastPrice - _price1Minute.LastPrice;
            double baDeltaChange = _bidAskDelta - _bidAskDelta1Minute;

            // Do not want to divide by zero.
            if (priceChange != 0)
            {
                int ratio = (int) Math.Round(baDeltaChange / priceChange);
                lblRatio.Text = ratio.ToString();
                
                    if (ratio > 0)
                    lblRatio.BackColor = Color.Green;
                else if (ratio < 0)
                    lblRatio.BackColor = Color.Red;
            }

            lblPriceChange.Text = priceChange.ToString();
            lblBADeltaChange.Text = baDeltaChange.ToString();
        }


    }   // TraderForm
}