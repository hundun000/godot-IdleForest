
与Unity实现的比较：

不使用BaseHundunScreen，直接使用GodotBaseHundunScreen  

|godot方法				|godot备注				|(Unity)hundunlib方法				|Unity备注	|用途	|
|---		|---		|---			|---			|---			|
|Godot消息 void _Ready()	|子节点先执行	| 此处对应的是show()中 postPrefabInitialization() 之后的部分（例如lazyInitLogicContext()） | Unity消息 void Start()  -> void show() |                                                              |
| Godot消息 void _EnterTree()           | 父节点先执行 | 此处对应的是show()中 postPrefabInitialization() （自身处理+lazyInitUiRootContext处理子节点 ） |	|优先使用Godot的开发习惯：各个_EnterTree仅处理自身，不再需要lazyInitUiRootContext以手工调用处理子节点，由Godot引擎来实现节点树遍历。	|
|Godot消息 void _Process(double delta)||void render(float delta)|	|	|
|void onLogicFrame()||void onLogicFrame()|	|逻辑帧（每1s）	|

