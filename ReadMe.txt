프로그램 흐름도

1. 시작
1-1. 프로그램 초기화

2. 씬 매니저에 씬 추가
2-1. 각 씬 초기 엔티티 추가및 초기화

-Loop
3. Scene Awake
3-1. Entity Remove
3-2. UpdatePriority

4. Scene Start

5.Input
5-1.InputCheck
5-2. if input == Esc
=> CommandInput

6. Scene Update
6-1. if sceneStatus == sceneChange
=> nowRunningScene Change And Init

7. Rendering
7-1. Rendering.Priority
-Loop
