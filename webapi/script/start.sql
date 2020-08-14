﻿CREATE DATABASE coupman; 
\c coupman
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Parties" (
    "PartyId" bigint NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    "Category" text NULL,
    "Description" text NULL,
    CONSTRAINT "PK_Parties" PRIMARY KEY ("PartyId")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200810231350_Initial', '3.1.6');
