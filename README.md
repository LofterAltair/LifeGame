生命游戏：
生命游戏中，对于任意细胞，规则如下：
每个细胞有两种状态-存活或死亡，每个细胞与以自身为中心的周围八格细胞产生互动。（如图，黑色为存活，白色为死亡）

当前细胞为死亡状态时，当周围有3个存活细胞时，该细胞变成存活状态。 （模拟繁殖）
当前细胞为存活状态时，当周围低于2个（不包含2个）存活细胞时， 该细胞变成死亡状态。（模拟人口稀少）
当前细胞为存活状态时，当周围有2个或3个存活细胞时， 该细胞保持原样。
当前细胞为存活状态时，当周围有3个以上的存活细胞时，该细胞变成死亡状态。（模拟过度拥挤）
