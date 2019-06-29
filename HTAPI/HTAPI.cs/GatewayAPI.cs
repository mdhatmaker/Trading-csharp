using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTAPI;

namespace HTAPI
{
    public class GatewayAPI
    {
        public delegate void ExchangeStatusUpdate(string sExchange, string sText, int bOpenned, int bServerUp);
        public delegate void StatusUpdate(int lHintMask, string sText);
        public delegate void ExchangeMessage(string sExchange, string sTimeStamp, string sInfo, string sText);
        public event ExchangeStatusUpdate OnExchangeStatusUpdate;
        public event StatusUpdate OnStatusUpdate;
        public event ExchangeMessage OnExchangeMessage;

        #region Declare the XTAPI objects
        private TTGate _ttGate = null;
        private List<string> _gatewayStatus;
        #endregion

        public GatewayAPI()
        {
            // This List will contain all of the available TT Gateways and their current state.
            _gatewayStatus = new List<string>();

            // Instantiate the TTGate.
            _ttGate = new XTAPI.TTGate();
            // Subscribe to the OnExchangeStateUpdate.
            _ttGate.OnExchangeStateUpdate += new _ITTGateEvents_OnExchangeStateUpdateEventHandler(TTGate_OnExchangeStateUpdate);
            _ttGate.OnStatusUpdate += new _ITTGateEvents_OnStatusUpdateEventHandler(TTGate_OnStatusUpdate);
            _ttGate.OnExchangeMessage += new _ITTGateEvents_OnExchangeMessageEventHandler(TTGate_OnExchangeMessage);
        }

        /// <summary>
        /// This event is triggered when the state of a
        ///	TT Gateway changes.
        /// </summary>
        private void TTGate_OnExchangeStateUpdate(string sExchange, string sText, int bOpenned, int bServerUp)
        {
            if (OnExchangeStatusUpdate != null) OnExchangeStatusUpdate(sExchange, sText, bOpenned, bServerUp);

            // Output the parameters to the user interface.
            //txtOnExchangeStateUpdateOutput.Text += sExchange + ",  " + sText + ",  " + bOpenned + ",  " + bServerUp + "\r\n";

            // Add the exchange and state to the ArrayList.
            // Note: The ArrayList simply keeps track of the current state
            // of each TT Gateway.
            string functionParameters = sExchange + "," + sText + "," + bOpenned.ToString() + "," + bServerUp.ToString();
            if (_gatewayStatus.Contains(functionParameters) == false)
            {
                if (_gatewayStatus.Count > 0)
                {
                    System.Collections.IEnumerator myEnum = _gatewayStatus.GetEnumerator();
                    while (myEnum.MoveNext())
                    {
                        Array gatewayInfo = myEnum.Current.ToString().Split(',');
                        if (gatewayInfo.GetValue(0).ToString() == sExchange && gatewayInfo.GetValue(1).ToString() == sText)
                        {
                            _gatewayStatus.Remove(myEnum.Current.ToString());
                            break;
                        }
                    }
                }
                _gatewayStatus.Add(functionParameters);
            }


            /*// Add the exchange to the combo box if it doesn't yet exist.
            if (cboAvailableGateways.Items.Contains(sExchange) == false)
            {
                cboAvailableGateways.Items.Add(sExchange);
                cboAvailableGateways.SelectedIndex = 0;
            }*/

            // Update the user interface.
            //gatewayStatusUpdate(sText, bOpenned, bServerUp);
        }

        /// <summary>
        /// This event is triggered when connection
        ///	with X_TRADER changes.
        /// </summary>
        private void TTGate_OnStatusUpdate(int lHintMask, string sText)
        {
            if (OnStatusUpdate != null) OnStatusUpdate(lHintMask, sText);

            //txtOnStatusUpdateOutput.Text += lHintMask + ",  " + sText + "\r\n";

            if (lHintMask == 1)	// X_TRADER is connected
            {
                //lblXTraderValue.Text = "Connected";
            }
            if (lHintMask == 32)	// X_TRADER 
            {
                //lblXTraderProValue.Text = "Available";
            }
        }

        /// <summary>
        /// This event is triggered when the gateway
        ///	sends a informational message
        /// </summary>
        private void TTGate_OnExchangeMessage(string sExchange, string sTimeStamp, string sInfo, string sText)
        {
            if (OnExchangeMessage != null) OnExchangeMessage(sExchange, sTimeStamp, sInfo, sText);
            //txtOnExchangeMessageOutput.Text += sExchange + ",  " + sTimeStamp + ",  " + sInfo + ",  " + sText + "\r\n";
        }
    } //class
} //namespace
