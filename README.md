

不使用BaseHundunScreen，直接使用GodotBaseHundunScreen  

|godot方法				|godot备注				|(Unity)hundunlib方法				|用途	|
|---		|---		|---			|---			|
|Godot消息 void _Ready()	|子节点先执行	| Unity消息 void Start()  -> void show() -> postPrefabInitialization() 之后的部分 |                      |
| Godot消息 void _EnterTree()           | 父节点先执行 | Unity消息 void Start()  -> void show() -> Screen自上而下调用组件树postPrefabInitialization() |依赖父组件初始化自身	|
|Godot消息 void _Process(double delta)||void render(float delta)|	|
|void onLogicFrame()||void onLogicFrame()|逻辑帧（每1s）	|

