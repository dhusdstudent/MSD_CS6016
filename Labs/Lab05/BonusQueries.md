QUESTION: 
Find the Titles of the library's oldest `<N>` books. Assume the lowest serial number is the oldest book.

QUERY:
SELECT title FROM titles JOIN inventory ON titles.isbn = inventory.isbn ORDER BY inventory.serial LIMIT 3;

QUESTION:
Find the name of the person who has checked out the most recent book. Assume the highest serial number is the newest book. Hint: the highest serial number book may not be checked out by anyone.

QUERY: 
SELECT name FROM patrons JOIN checkedout ON checkedout.cardnum = patrons.cardnum ORDER BY checkedout.serial DESC LIMIT 1;

QUESTION:
Find the phone number(s) of anyone who has not checked out any books.
QUERY:
SELECT phone FROM phones JOIN patrons ON patrons.cardnum = phones.cardnum LEFT JOIN checkedout ON checkedout.cardnum = phones.cardnum WHERE checkedout.cardnum IS NULL;

QUESTION:
The library wants to expand the number of unique selections in its inventory, thus, it must know the ISBN and Title of all books that it owns at least one copy of. Create a query that will return the ISBN and Title of every book in the library, but will not return the same book twice.

QUERY:
SELECT DISTINCT titles.isbn, titles.title FROM titles JOIN inventory ON titles.isbn = inventory.isbn;

QUESTION:
Find the name of the patron who has checked out the most books.
QUERY:
SELECT name FROM patrons JOIN checkedout ON patrons.cardnum = checkedout.cardnum GROUP BY patrons.cardnum, patrons.name ORDER BY COUNT(*) DESC LIMIT 1;

QUESTION:
Find the Authors who have written more than one book. Assume that two Authors with the same name are the same Author for this query.
QUERY:
SELECT author, COUNT(*) FROM titles GROUP BY author HAVING COUNT(*) > 1;

QUESTION:
Find the Authors for which the library has more than one book in inventory (this includes multiple copies of the same book). Assume that two Authors with the same name are the same Author for this query.

QUERY:
SELECT author, COUNT(*) FROM titles JOIN inventory ON inventory.isbn = titles.isbn GROUP BY author, inventory.isbn HAVING COUNT(*) > 1;

QUESTION:
The library wants to implement a customer loyalty program based on how many books each patron has checked out. Provide an SQL query that returns the names, number of books they have checked out, and loyalty level of each Patron. The loyalty level should be the string "Platinum" if they have checked out > 2 books, "Gold" if they have 2 books, "Silver" if they have 1 book, and "Bronze" if they have no books. Hint: remember that NULL represents an unknown in SQL (it does not represent 0).

QUERY:
SELECT patrons.name, COUNT(*), CASE
	WHEN COUNT(*) > 2 THEN 'Platinum'
	WHEN COUNT(*) = 2 THEN 'Gold'
	WHEN COUNT(*) = 1 THEN 'Silver'
	ELSE 'Bronze'
END AS loyalty_level 
FROM patrons JOIN checkedout ON patrons.cardnum = checkedout.cardnum GROUP BY patrons.name;

QUESTION:
Find the name of the oldest book by each author. By oldest, we mean the book with the smallest serial number.

QUERY:
SELECT DISTINCT ON (author) title, author FROM titles JOIN inventory ON inventory.isbn = titles.isbn ORDER BY author, serial DESC; 