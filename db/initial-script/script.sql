SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS practice;

CREATE DATABASE practice WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


\connect practice

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;


CREATE FUNCTION public.uuid_generate_v4() RETURNS uuid
    LANGUAGE c STRICT PARALLEL SAFE
    AS '$libdir/uuid-ossp', 'uuid_generate_v4';


SET default_tablespace = '';

SET default_table_access_method = heap;


CREATE TABLE public.role (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(100),
    active boolean,
    description character varying(255),
    created_by character varying(50),
    created_date timestamp(3) without time zone,
    create_program character varying(50),
    updated_by character varying(50),
    updated_date timestamp(3) without time zone,
    updated_program character varying(50),
    code character varying(10)
);

ALTER TABLE ONLY public.role
    ADD CONSTRAINT pk_role PRIMARY KEY (id);


INSERT INTO public."role"
("name", active, description, created_by, created_date, create_program, updated_by, updated_date, updated_program, code)
VALUES('admin', true, 'manage all module', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'admin');
INSERT INTO public."role"
("name", active, description, created_by, created_date, create_program, updated_by, updated_date, updated_program, code)
VALUES('user', true, 'create transaction', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'admin');
INSERT INTO public."role"
("name", active, description, created_by, created_date, create_program, updated_by, updated_date, updated_program, code)
VALUES('super user', true, 'create master and manage module', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'sql', '2024-05-18 16:02:38.201', 'migrate', 'admin');


CREATE TABLE public."user" (
	id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
	first_name varchar NOT NULL,
	last_name varchar NOT NULL,
	gender bool NOT NULL
);

ALTER TABLE ONLY public.user
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


INSERT INTO public."user"
(first_name, last_name, gender)
VALUES('Monkey', 'Luffy', true);
INSERT INTO public."user"
(first_name, last_name, gender)
VALUES('Tonytony', 'Chopper', true);
INSERT INTO public."user"
(first_name, last_name, gender)
VALUES('Roronoa', 'Zoro', true);
INSERT INTO public."user"
(first_name, last_name, gender)
VALUES('Nico', 'Robin', false);