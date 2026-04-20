# 要件分割
- 名前、点数を入力する
	- 入力された値はユーザー定義のデータクラスオブジェクトとなる。
	- そのデータクラスオブジェクトはリスト内で管理される
- 集計されたデータは以下の3つを導くために使われる
	- 平均点
	- 最高点
	- 最低点
- また、条件抽出にもデータは用いられる
	- 60点以上を一覧表示する
# クラスとメソッド考案
## 1. class StudentScore (データクラス)
- 最初の代入でしか値を設定できない
-　string Name,int Scoreプロパティを持つ
## 2.class GradeBook(データ管理と直接操作クラス)
- データ管理クラス
- List\<StudentScore\>(全員用)をフィールドに持つ
	- これはIReadOnlyであるべき。
### 0. method AddStudentData
- 引数にstring nameとint scoreを受け取る
- 受け取った情報をもとにStudentScoreを作る。そして自分のクラスに追加する。
- 今回はID管理は必要ない
### 1. method GetAverage 
- 引数には何も取らない
- floatを返す
- sumで総和を求め、リスト内の.Countで割ればよさそう
- 最後に割る
### 2.method GetMaxScore
- 引数には何も取らない
- intを返す
- .maxを使って取り出す。foreachは使わないように
### 3. method GetMinScore
- 引数には何も取らない
- intを返す
- .minを使って取り出す
### 4. method GetPassStudents
- 引数には何も取らない。
- List \<StudentScore\>(合格者用)を返す
- ラムダで60点以上の生徒を抽出し、メソッド内で定義したリストに格納する。
## 3.class StudentService(入出力担当)
- フィールドにGradeBookオブジェクトを持つ
- このクラスは入力受付とそのための表示。処理指示を担当するクラス。
- だけど今回は前回ほど重くはないため、メソッドは一つで済みそうだ。
### 0. method AddCommand
- string nameとint scoreの入力を受け付けて正常値なら返す
- 今回もdo-whileで入力が正常化を確かめる。
	- string.IsNullOrWhiteSpace(name) || score < 0 || 100 < scoreでどうかな？
- 追加処理まで行わせようかな？それだと責務分離が少し崩れるかな。。。

### 1. method SelectMenu
- 引数には何も取らない。
- stringを返す。入力を大文字に整形したものを返そう。
- リストで受け入れる入力を限定し、そうでなければループさせるdo-whileでいこう。
## 4. class StudentScoreFlow(総司令)
### 0. Main
- ここでMenu操作を請け負う。
- 3.1メソッドに入力を請け負わせてその返り値をもとに処理を開始しよう。普通にif文で！

# ポイント
- 前回と違うポイントは倉庫内でデータを特定するメソッドがそのまま値を返すため、あまりクラスやメソッドが必要ない事。