# 要件分割
- 個人のデータクラスを作る必要がある。
- 入金、出金が出来る。
- 残高の表示が出来る。
- 過去の操作の履歴をリストで表示できる。

# クラスとメソッド考察
## class BankAcount
### フィールド：name,balance,history
	- nameはstring.balanceはint,
	- なんということだ。操作履歴もインスタンスごとに持っていた方がよいではないか。
	- historyフィールドも持ったほうが良い。List<string>で操作をそれぞれ保存する
### プロパティ：BankAcount(name,balance,history)
	- nameは入力で空文字入力を防ぐため特にガードは必要ないだろう。
	- balanceは負の値にならないようにプロパティで防ぐ必要性がある
	- historyは特定のメソッドからしか操作できないようにしたい。
	- historyのみ、何度でも変更を受け入れる必要がある。
	- Adminは残高、履歴を持つ必要がないためアカウントを持たない
### method bool DepositAcount(paidIn)
	- ここでは引数に入金額を受け取る
	- インスタンスを引き出してきて、balance+paid
	- 履歴として残す必要がある。$"入金：{入金変数}"ってインスタンス内のリストに加えればいい
	- 処理の成功を示すboolを返す
### method bool WithdrawAcount( paidOut)
	- 入力されたぶんの金額を残高から減らす。
	- こちらも履歴のリストに記録を残すようにする必要がある
	- こちらも同じく処理の成功を示すboolを返す
## class ConsoleHandler
### method void DepositCommand()
	- 入金後の残高を表示する。
### method void WithdrawCommand()
	- 引き下げ後の残高を表示する。
### method void  ViewHistory()
	- 今までの履歴を.ForEachで一覧表示
### method string MenuCommand()
	- 操作入力による文字列を返す

## class BankRunner
### method Main(string args[])
	- ループで入力を受けつ、操作をする。
	- Qを押されたときにプログラムの終了が出来るようにする
# メモ
学んだこと：自分で要件を加えない
