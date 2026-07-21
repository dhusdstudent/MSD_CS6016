-----------------------------------------PART 1 - SELECTING INDEXES-----------------------------------------

--> DATABASE QUESTION 1
This table would need an additional compound key of StartDate and EndDate.

--> DATABASE QUESTION 2
This table would need Grade added as an index in addition to the studentID and className.

--> DATABASE QUESTION 3
For these particular queries, you'd need className and Grade as an index.

--> DATABASE QUESTION 4
You'd need Elo (from Players) and WhitePlayer (from Games).

--> DATABASE QUESTION 5
You would need to index whatever you're 'naturally joining' on. Presumably this would be the ISBN or serialNum.

--> DATABASE QUESTION 6
You'd need CheckedOut(CardNum) as an index.

--> DATABASE QUESTION 7
You would need to index the book's identification number in Serials (ISBN or serialNum)

-----------------------------------------PART 2 - B+ TREE INDEX STRUCTURES-----------------------------------------
--> STUDENTS TABLE
    Q: How many rows of the table can be placed into the first leaf node of the primary index before it 
will split?

    A: 4096 / 15 = 273.

    Q: What is the maximum number of keys stored in an internal node of the primary index? (Remember to 
ignore pointer space. Remember that internal nodes have a different structure than leaf nodes.)

    A: 4096 / 14 = 292.

    Q: What is the maximum number of rows in the table if the primary index has a height of 1? (A tree of 
height 1 has 2 levels)

    A: (292 + 1) * 273 = 79989

    Q: What is the minimum number of rows in the table if the primary index has a height of 1? (A tree of 
height 1 has 2 levels)

    A: (273 / 2) * 2 = 274

    Q: If there is a secondary index on Grade, what is the maximum number of entries a leaf node can hold 
in the secondary index?

    A: 4096

--> ANOTHER TABLE

    Q: What is the maximum number of leaf nodes in the primary index if the table contains 48 rows?

    A: 4096 / 128 = 32

    Q: What is the minimum number of leaf nodes in the primary index if the table contains 48 rows?

    A: 48 / (23 /2) = 3