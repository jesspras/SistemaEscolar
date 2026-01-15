CREATE DATABASE SistemaEscolar;
GO

USE SistemaEscolar;
GO

CREATE TABLE Turmas (
    TurmaId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Turno NVARCHAR(20) NOT NULL, -- Manhã, Tarde, Integral
    HoraInicio TIME NOT NULL,
    HoraFim TIME NOT NULL
);

CREATE TABLE Professoras (
    ProfessoraId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    CPF NVARCHAR(14) NOT NULL UNIQUE,
    Rua NVARCHAR(100),
    Bairro NVARCHAR(60),
    CEP NVARCHAR(10),
    Telefone NVARCHAR(20),
    DataNascimento DATE,
    Formacao NVARCHAR(100)
);
CREATE TABLE TurmaProfessoras (
    TurmaId INT NOT NULL,
    ProfessoraId INT NOT NULL,

    CONSTRAINT PK_TurmaProfessoras PRIMARY KEY (TurmaId, ProfessoraId),

    CONSTRAINT FK_TP_Turma FOREIGN KEY (TurmaId)
        REFERENCES Turmas(TurmaId)
        ON DELETE CASCADE,

    CONSTRAINT FK_TP_Professora FOREIGN KEY (ProfessoraId)
        REFERENCES Professoras(ProfessoraId)
        ON DELETE CASCADE
);
CREATE TABLE Alunos (
    AlunoId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    Cor NVARCHAR(30),
    Sexo NVARCHAR(20),
    CPF NVARCHAR(14),

    Rua NVARCHAR(100),
    Bairro NVARCHAR(60),
    CEP NVARCHAR(10),

    NomeMae NVARCHAR(100),
    TelefoneMae NVARCHAR(20),
    NomePai NVARCHAR(100),
    TelefonePai NVARCHAR(20),

    TurmaId INT NOT NULL,

    CONSTRAINT FK_Aluno_Turma FOREIGN KEY (TurmaId)
        REFERENCES Turmas(TurmaId)
);
CREATE TABLE PessoasAutorizadas (
    PessoaAutorizadaId INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    AlunoId INT NOT NULL,

    CONSTRAINT FK_PessoaAutorizada_Aluno FOREIGN KEY (AlunoId)
        REFERENCES Alunos(AlunoId)
        ON DELETE CASCADE
);
CREATE TABLE Pareceres (
    ParecerId INT IDENTITY(1,1) PRIMARY KEY,
    AlunoId INT NOT NULL,
    Ano INT NOT NULL,
    Semestre INT NOT NULL, -- 1 ou 2
    CaminhoArquivo NVARCHAR(255) NOT NULL,

    CONSTRAINT FK_Parecer_Aluno FOREIGN KEY (AlunoId)
        REFERENCES Alunos(AlunoId),

    CONSTRAINT UQ_Parecer UNIQUE (AlunoId, Ano, Semestre)
);
CREATE TABLE HistoricosEscolares (
    HistoricoEscolarId INT IDENTITY(1,1) PRIMARY KEY,
    AlunoId INT NOT NULL,
    Ano INT NOT NULL,
    CaminhoArquivo NVARCHAR(255) NOT NULL,

    CONSTRAINT FK_Historico_Aluno FOREIGN KEY (AlunoId)
        REFERENCES Alunos(AlunoId),

    CONSTRAINT UQ_Historico UNIQUE (AlunoId, Ano)
);
CREATE TABLE Frequencias (
    FrequenciaId INT IDENTITY(1,1) PRIMARY KEY,
    AlunoId INT NOT NULL,
    Mes INT NOT NULL,
    Ano INT NOT NULL,

    DiasLetivos INT NOT NULL,
    Faltas INT NOT NULL,
    PercentualPresenca DECIMAL(5,2) NOT NULL,

    CONSTRAINT FK_Frequencia_Aluno FOREIGN KEY (AlunoId)
        REFERENCES Alunos(AlunoId),

    CONSTRAINT UQ_Frequencia UNIQUE (AlunoId, Mes, Ano)
);
CREATE INDEX IDX_Aluno_Nome ON Alunos(Nome);
CREATE INDEX IDX_Professoras_Nome ON Professoras(Nome);
CREATE INDEX IDX_Turma_Nome ON Turmas(Nome);
