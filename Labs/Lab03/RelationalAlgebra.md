
----------------------
#1 - $ T1 \bowtie_{T1.A = T2.A} T2 $

SCHEMA - (T1.A, Q, R, T2.A, B, C)

| T1.A         |         Q          |            R | T2.A         |         B          |            C |
|:-------------|:------------------:|-------------:|:-------------|:------------------:|-------------:|
| 20           |         a          |            5 | 20           |         b          |            6 |
| 20           |         a          |            5 | 20           |         b          |            5 |


#2 - $ T1 \bowtie_{T1.Q = T2.B} T2 $

SCHEMA - (T1.A, Q, R, T2.A, B, C)

| T1.A |         Q          |            R | T2.A         |         B          |            C |
|:-----|:------------------:|-------------:|:-------------|:------------------:|-------------:|
| 25   |         b          |            8 | 20           |         b          |            6 |
| 25   |         b          |            8 | 20           |         b          |            5 |


#3 - $ T1 \bowtie T2 $

SCHEMA - (A, Q, R, B, C)

| A  | Q | R | B | C |
|:---|:-:|--:|:--|:-:|
| 20 | a | 5 | b | 6 |
| 20 | a | 5 | b | 5 |


#4 - $ T1 \bowtie_{T1.A = T2.A \wedge T1.R = T2.C} T2 $

SCHEMA -(T1.A, Q, R, T2.A, B, C)

| T1.A |         Q          |            R | T2.A         |         B          |            C |
|:-----|:------------------:|-------------:|:-------------|:------------------:|-------------:|
| 20           |         a          |            5 | 20           |         b          |            5 |


----------------------
$\pi$.  $\sigma$. $\cup$. $\cap$. $\vee$.  $\wedge$.  


Find the names of any player with an Elo rating of 2850 or higher:

$\pi_{Name}(\sigma_{elo >= 2850}(Players))$

Find the names of any player who has ever played a game as white.

$ \pi_{Name}(Players \bowtie_{Players.pID = Games.wpID} Games) $

Find the names of any player who has ever won a game as white.

$ \pi_{Name}(\sigma_{Result=1-0}(Players \bowtie_{Players.pID = Games.wpID } Games)) $

Find the names of any player who played any games in 2018.

$ \pi_{Players.Name}(\sigma_{Year=2018}(Events \bowtie(Players \bowtie_{Players.pID = Games.wpID \vee Players.pID = Games.bpID} Games))) $

Find the names and dates of any event in which Magnus Carlsen lost a game.

$ \pi_{Events.Name, Events.Year}(\sigma_{(Games.wpID = 1 \wedge Result = '0-1') /vee (Games.bpID = 1 \wedge Result = '1-0')}(Events  \bowtie(Players \bowtie_{Players.pID = Games.wpID \vee Players.pID = Games.bpID} Games)) $

Find the names of all opponents of Magnus Carlsen. An opponent is someone who he has played a game against. Hint: Both Magnus and his opponents could play as white or black.

$ \pi_{O.Name}(\sigma_{M.Name='Magnus Carlsen'}((\rho_{M}(Players) \bowtie_{M.pID=Games.wpID} Games) \bowtie_{O.pID=Games.bpID} \rho_{O}(Players)))) $
$\cup$
$ \pi_{O.Name}(\sigma_{M.Name='Magnus Carlsen'}((\rho_{M}(Players) \bowtie_{M.pID=Games.bpID} Games) \bowtie_{O.pID=Games.wpID} \rho_{O}(Players)))) $


----------------------

Students

| sID |   Name   |  DOB |
|:----|:--------:|-----:|
| 1   | Hermione | 1980 |
| 2   |  Harry   | 1979 |
| 3   |   Ron    | 1980 |
| 4   |  Malfoy  | 1982 |

Enrolled

| sID | cID  | Grd |
|:----|:----:|----:|
| 1   | 3500 |   A |
| 1   | 3810 |  A- |
| 1   | 5530 |   A |
| 2   | 3810 |   A |
| 2   | 5530 |   B |
| 3   | 3500 |   C |
| 3   | 3810 |   B |
| 4   | 3500 |   C |

Courses

| cID  |     Name     |
|:-----|:------------:|
| 3500 | SW Practice  |
| 3810 | Architecture | 
| 5530 |  Databases   |

3.1

$ \rho(C, \pi_{sid}(\sigma_{Grd=C}(Enrolled)))$

To begin with, we're renaming the following table to be represented by 'C':

| sID |
|:----|
| 3   |
| 4   |

$ \pi_{Name}((\pi_{sID}(Enrolled)-C)\bowtie Students)$

Now, you start by projecting all the sIDs (removing duplicates), then removing thos with the entries matching C (above).
You join Enrolled with Students, and project the names that remain.

| Name     |
|:---------|
| Hermione |
| Harry    |

3.2

$ \rho(S1, Students) $

$ \rho(S2, Students) $

$ \pi_{S2.Name}(\sigma_{S1.Name == Ron \wedge S1.DOB == S2.DOB \wedge S2.name != Ron}(S1 \times S2))$

This equation creates a cross product between s1 and s2, which were two rho-named copies made of the original student table. 
It then removes entries based on three factors: 
1. The s1 name has to be Ron.
2. The s1 birthdate must match the s2 birthdate.
3. The s2 name cannot be Ron.

After determining the entries (or entry, in this case) left, it asks us to present the s2 name.

| s2.Name  |
|:-------|
| Hermione |


3.3

$ \pi_{Name}((\pi_{cID,sID}(Enrolled)/\pi_{sID}(Students)) \bowtie Courses)$

To begin with, we select only the cID and sID from the Enrolled table, select only the sID from the Students table,
and divide them. There are no tuples. As such, when we join it with courses, this results in the table being empty.

We ultimately cannot return the names of the courses that are taken by every student, because there are none.

----------------------

Part 4

$ \pi_{Name}((\pi_{cID,sID}(Enrolled)/\pi_{cID}(\sigma_{cID > 3000 \wedge cID < 4000}(Courses))) \bowtie Students)$