﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using QuantConnect;
using QuantConnect.Data;
using System;
using NodaTime;
using QuantConnect.Data.Custom;
using QuantConnect.Data.Custom.Tiingo;
using QuantConnect.Indicators;

namespace QuantConnect.Algorithm.CSharp
{
    /// <summary>
    /// This example algorithm shows how to import and use Tiingo daily prices data.
    /// </summary>
    /// <meta name="tag" content="strategy example" />
    /// <meta name="tag" content="using data" />
    /// <meta name="tag" content="custom data" />
    /// <meta name="tag" content="tiingo" />
    public class TiingoDailyDataAlgorithm : QCAlgorithm
    {
        //    private const string tiingoTicker = "AAPL";
        private const string energyTicker = "EBA.AZPS-ALL.D.H";  // US nuclear capacity outage (Daily)
                                                                 //  private Symbol _tiingoSymbol;
        private Symbol _energySymbol;

        //private ExponentialMovingAverage _emaFast;
        //private ExponentialMovingAverage _emaSlow;

        /// <summary>
        /// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
        /// </summary>
        public override void Initialize()
        {
            SetStartDate(2019, 4, 18);
            //            SetEndDate(Time.EndOfTime);
            SetCash(100000);

            // Set your Tiingo API Token here
            //          Tiingo.SetAuthCode("d73db18afc9589fb750d39da8c9f3cf721375d1a");
            // Set your US Energy Information Administration (EIA) Token here
            USEnergyInformation.SetAuthCode("062b70da94eece0c736dedc02f8a63be");

            //            _tiingoSymbol = AddData<TiingoDailyData>(tiingoTicker, Resolution.Daily).Symbol;
            _energySymbol = AddData<USEnergyInformation>(energyTicker, Resolution.Hour, DateTimeZone.Utc, false, 1.0m).Symbol;

            //  _emaFast = EMA(_tiingoSymbol, 5);
            //_emaSlow = EMA(_tiingoSymbol, 10);
        }

        /// <summary>
        /// OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.
        /// </summary>
        /// <param name="slice">Slice object keyed by symbol containing the stock data</param>
        public override void OnData(Slice slice)
        {
            // Extract Tiingo data from the slice
            //var tiingoData = slice.Get<TiingoDailyData>();
            //foreach (var row in tiingoData.Values)
            //{
            //    Log($"{Time} - {row.Symbol.Value} - {row.Close} {row.Value} {row.Price} - EmaFast:{_emaFast} - EmaSlow:{_emaSlow}");
            //}
            foreach (var key in slice.Keys)
            {
                Log($"Key: {key.Value}");
            }
            // Extract US EIA data from the slice
            var energyData = slice.Get<USEnergyInformation>();
            foreach (var row in energyData.Values)
            {
                Log($"{Time} - {row.Symbol.Value} - {row.Value} ");
            }


            // Simple EMA cross
            //if (!Portfolio.Invested && _emaFast > _emaSlow)
            //{
            //    SetHoldings(_tiingoSymbol, 1);
            //}
            //else if (Portfolio.Invested && _emaFast < _emaSlow)
            //{
            //    Liquidate(_tiingoSymbol);
            //}
        }
    }
}