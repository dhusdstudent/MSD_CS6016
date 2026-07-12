--
-- PostgreSQL database dump
--

\restrict JaJL2zFgXPZRMGUB2M5RZ3SG4l8lNMPrmPuReShRBALH1gy8qMFIHejOodvfL3N

-- Dumped from database version 18.3 (Debian 18.3-1.pgdg13+1)
-- Dumped by pg_dump version 18.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: checkedout; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.checkedout (
    cardnum integer NOT NULL,
    serial integer NOT NULL
);


ALTER TABLE public.checkedout OWNER TO postgres;

--
-- Name: inventory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inventory (
    serial integer NOT NULL,
    isbn character(14) NOT NULL
);


ALTER TABLE public.inventory OWNER TO postgres;

--
-- Name: inventory_serial_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.inventory_serial_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.inventory_serial_seq OWNER TO postgres;

--
-- Name: inventory_serial_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.inventory_serial_seq OWNED BY public.inventory.serial;


--
-- Name: patrons; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.patrons (
    cardnum integer NOT NULL,
    name character varying(100) NOT NULL
);


ALTER TABLE public.patrons OWNER TO postgres;

--
-- Name: patrons_cardnum_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.patrons_cardnum_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.patrons_cardnum_seq OWNER TO postgres;

--
-- Name: patrons_cardnum_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.patrons_cardnum_seq OWNED BY public.patrons.cardnum;


--
-- Name: phones; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.phones (
    cardnum integer NOT NULL,
    phone character(8) NOT NULL
);


ALTER TABLE public.phones OWNER TO postgres;

--
-- Name: phones_cardnum_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.phones_cardnum_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.phones_cardnum_seq OWNER TO postgres;

--
-- Name: phones_cardnum_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.phones_cardnum_seq OWNED BY public.phones.cardnum;


--
-- Name: titles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.titles (
    isbn character(14) NOT NULL,
    title character varying(100) NOT NULL,
    author character varying(100) NOT NULL
);


ALTER TABLE public.titles OWNER TO postgres;

--
-- Name: inventory serial; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventory ALTER COLUMN serial SET DEFAULT nextval('public.inventory_serial_seq'::regclass);


--
-- Name: patrons cardnum; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.patrons ALTER COLUMN cardnum SET DEFAULT nextval('public.patrons_cardnum_seq'::regclass);


--
-- Name: phones cardnum; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.phones ALTER COLUMN cardnum SET DEFAULT nextval('public.phones_cardnum_seq'::regclass);


--
-- Data for Name: checkedout; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.checkedout (cardnum, serial) FROM stdin;
1	1001
1	1004
4	1005
4	1006
\.


--
-- Data for Name: inventory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inventory (serial, isbn) FROM stdin;
1006	978-0062278791
1010	978-0261102309
1004	978-0394823379
1005	978-0394823379
1001	978-0547928227
1002	978-0547928227
1008	978-0553283686
1009	978-0553283689
1003	978-0679732242
\.


--
-- Data for Name: patrons; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.patrons (cardnum, name) FROM stdin;
1	Joe
2	Ann
3	Ben
4	Dan
\.


--
-- Data for Name: phones; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.phones (cardnum, phone) FROM stdin;
1	555-5555
2	555-5555
2	666-6666
3	111-1111
4	888-8888
4	999-9999
\.


--
-- Data for Name: titles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.titles (isbn, title, author) FROM stdin;
978-0062278791	Profiles in Courage	Kennedy
978-0261102309	The Lord of the Rings	Tolkien
978-0394823379	The Lorax	Seuss
978-0441172719	Dune	Herbert
978-0547928227	The Hobbit	Tolkien
978-0553283686	Hyperion	Simmons
978-0553283689	Endymion	Simmons
978-0679732242	The Sound and the Fury	Faulkner
\.


--
-- Name: inventory_serial_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.inventory_serial_seq', 1, false);


--
-- Name: patrons_cardnum_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.patrons_cardnum_seq', 1, false);


--
-- Name: phones_cardnum_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.phones_cardnum_seq', 1, false);


--
-- Name: checkedout checkedout_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.checkedout
    ADD CONSTRAINT checkedout_pkey PRIMARY KEY (serial);


--
-- Name: inventory inventory_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventory
    ADD CONSTRAINT inventory_pkey PRIMARY KEY (serial);


--
-- Name: patrons patrons_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.patrons
    ADD CONSTRAINT patrons_pkey PRIMARY KEY (cardnum);


--
-- Name: phones phones_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.phones
    ADD CONSTRAINT phones_pkey PRIMARY KEY (cardnum, phone);


--
-- Name: titles titles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.titles
    ADD CONSTRAINT titles_pkey PRIMARY KEY (isbn);


--
-- Name: Inventory_ISBN_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "Inventory_ISBN_idx" ON public.inventory USING btree (isbn);


--
-- Name: checkedout_cardnum_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX checkedout_cardnum_idx ON public.checkedout USING btree (cardnum);


--
-- Name: checkedout checkedout_ibfk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.checkedout
    ADD CONSTRAINT checkedout_ibfk_1 FOREIGN KEY (cardnum) REFERENCES public.patrons(cardnum);


--
-- Name: checkedout checkedout_ibfk_2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.checkedout
    ADD CONSTRAINT checkedout_ibfk_2 FOREIGN KEY (serial) REFERENCES public.inventory(serial);


--
-- Name: inventory inventory_ibfk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventory
    ADD CONSTRAINT inventory_ibfk_1 FOREIGN KEY (isbn) REFERENCES public.titles(isbn);


--
-- Name: phones phones_ibfk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.phones
    ADD CONSTRAINT phones_ibfk_1 FOREIGN KEY (cardnum) REFERENCES public.patrons(cardnum) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict JaJL2zFgXPZRMGUB2M5RZ3SG4l8lNMPrmPuReShRBALH1gy8qMFIHejOodvfL3N

