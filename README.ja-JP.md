# Financier
[English](README.md)  

Financier は、金融アプリケーションのためのフレームワークです。
- 金融アプリケーションで使用されるオブジェクトモデルを定義しています。
- 各種オブジェクトモデルでは、一般的に利用されると思われるプロパティ、メソッドを定義しています。
- 日本語からの翻訳で曖昧になりがちな英語による定義名の標準化を目指しています。

### Financier.Core
Financier.Core は、各種金融アプリケーションで共通的に利用されると思われる定義を含んでいます。

```
PM> Install-Package Financier.Core
```

#### インジケーター
Reactive Extensions (Rx) ベースのインジケーター群
- [Simple Moving Average (SMA)](Financier.Core/Indicators/SimpleMovingAverage.cs)
- [Modified Moving Average (MMA)](Financier.Core/Indicators/ModifiedMovingAverage.cs)
- [Exponentioal Moving Average (EMA)](Financier.Core/Indicators/ExponentialMovingAverage.cs)
- [Triple Smoothed Exponential Moving Average (TRIX)](Financier.Core/Indicators/TripleSmoothedExponentialMovingAverage.cs)
- [Accumulation Distribution (ADI)](Financier.Core/Indicators/AccumulationDistribution.cs)
- [Chaikin Oscillator](Financier.Core/Indicators/ChaikinOscillator.cs)
- [Relative Strength Index (RSI)](Financier.Core/Indicators/RelativeStrengthIndex.cs)

#### シグナル

### Financier.Trading
Financier.Trading は、取引アプリケーションを作成する際に必要と思われる定義が含まれています。

### Financer.Date (非公開)
Business day conventions や Day count conventions を考慮した期間計算やキャッシュフロースケジュール生成などの機能を提供します。
- C# .NET Standard 2.0 ライブラリ
- Micorosoft Excel セル関数、VBA関数
- Excel Online セル関数
- Python ライブラリ

### Financier.Bonds (非公開)
日本国債、外国債、公債、社債等、各種債券の時価計算機能を提供します。
