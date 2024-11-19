//@version=5 
strategy("Bull Market Support Band Strategy", overlay=true )
source = close
smaLength = 20
emaLength = 21

sma = ta.sma(source, smaLength)
ema = ta.ema(source, emaLength)

outSma = request.security(syminfo.tickerid, timeframe.period, sma)
outEma = request.security(syminfo.tickerid, timeframe.period, ema)
 
smaPlot = plot(outSma, color=color.new(color.red, 0), title='20w SMA')
emaPlot = plot(outEma, color=color.new(color.green, 0), title='21w EMA')

fill(smaPlot, emaPlot, color=color.new(color.orange, 75), fillgaps=true)
  
// Entry and Exit Conditions (Customize based on your strategy)
enterLong = ta.crossover(ema, sma) // Enter long when EMA crosses above SMA
exitLong = ta.crossunder(sma, ema) // Exit long when SMA crosses below EMA

// Plot the bands and fill area for visualization
 
// Strategy Orders (Adjust risk management as needed)
// strategy.entry("Long Entry", strategy.long, when=enterLong)
// strategy.close("Long Exit", when=exitLong) 

if (ta.crossunder(ema, sma))
	strategy.entry("BBandLE", strategy.long, oca_name="BollingerBands", oca_type=strategy.oca.cancel, comment="buy")
 

if ( ta.crossover(ema, sma) )
	strategy.entry("BBandSE", strategy.short,  oca_name="BollingerBands", oca_type=strategy.oca.cancel, comment="sell")
 