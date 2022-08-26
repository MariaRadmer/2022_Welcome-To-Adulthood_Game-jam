Hello there! #speaker:Dr. Green #portrait:dr_green_neutral #layout:left
-> main

=== main ===
How are you feeling today?
+ [Happy]
    That makes me feel happy as well! #portrait:test_1
+ [Sad]
    Oh, well that makes me sad too. #portrait:test_2

- Don't trust him, he's not a real doctor! #speaker:Ms. Yellow #portrait:test_1 #layout:right

Well, do you have any more questions? #speaker:Dr. Green #portrait:test_2 #layout:left
+ [Yes]
    -> main
+ [No]
    Goodbye then!
    -> END
