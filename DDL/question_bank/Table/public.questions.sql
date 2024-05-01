-- Table: public.questions

-- DROP TABLE IF EXISTS public.questions;

CREATE TABLE IF NOT EXISTS public.questions
(
    question_id integer NOT NULL,
    exam_id character varying(255) COLLATE pg_catalog."default",
    exam_question_number character varying(255) COLLATE pg_catalog."default",
    grade character varying(255) COLLATE pg_catalog."default",
    subject character varying(255) COLLATE pg_catalog."default",
    question_type character varying(255) COLLATE pg_catalog."default",
    content character varying COLLATE pg_catalog."default",
    option character varying COLLATE pg_catalog."default",
    answer character varying COLLATE pg_catalog."default",
    parent_question_id integer,
    question_volume integer NOT NULL,
    update_datetime timestamp without time zone NOT NULL,
    update_user character varying(255) COLLATE pg_catalog."default" NOT NULL,
    create_datetime timestamp without time zone NOT NULL,
    create_user character varying(255) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT questions_pkey PRIMARY KEY (question_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.questions
    OWNER to postgres;

COMMENT ON TABLE public.questions
    IS '題目明細表';

COMMENT ON COLUMN public.questions.question_id
    IS '題目ID';

COMMENT ON COLUMN public.questions.exam_id
    IS '試卷ID';

COMMENT ON COLUMN public.questions.exam_question_number
    IS '試卷題號';

COMMENT ON COLUMN public.questions.grade
    IS '題目年級';

COMMENT ON COLUMN public.questions.subject
    IS '題目科目';

COMMENT ON COLUMN public.questions.question_type
    IS '題目類型 1:單選、2:多選、3:填充、4:問答、5:題組';

COMMENT ON COLUMN public.questions.content
    IS '題目內容';

COMMENT ON COLUMN public.questions.option
    IS '題目選項';

COMMENT ON COLUMN public.questions.answer
    IS '題目答案';

COMMENT ON COLUMN public.questions.parent_question_id
    IS '母題ID';

COMMENT ON COLUMN public.questions.question_volume
    IS '題目量';

COMMENT ON COLUMN public.questions.update_datetime
    IS '更新日期時間';

COMMENT ON COLUMN public.questions.update_user
    IS '更新使用者';

COMMENT ON COLUMN public.questions.create_datetime
    IS '建立時間';

COMMENT ON COLUMN public.questions.create_user
    IS '建立使用者';