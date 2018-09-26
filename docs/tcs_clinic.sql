-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema tcs_clinic
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema tcs_clinic
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `tcs_clinic` DEFAULT CHARACTER SET utf8 ;
USE `tcs_clinic` ;

-- -----------------------------------------------------
-- Table `tcs_clinic`.`person`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs_clinic`.`person` (
  `idperson` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `dateofbirth` DATETIME NULL,
  `gender` VARCHAR(1) NULL,
  `telephone` VARCHAR(45) NULL,
  `zipcode` VARCHAR(45) NULL,
  PRIMARY KEY (`idperson`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `tcs_clinic`.`doctor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs_clinic`.`doctor` (
  `iddoctor` INT NOT NULL,
  `specialty` VARCHAR(45) NULL,
  `salary` DOUBLE NULL,
  INDEX `fk_medico_pessoa1_idx` (`iddoctor` ASC),
  PRIMARY KEY (`iddoctor`),
  CONSTRAINT `fk_medico_pessoa1`
    FOREIGN KEY (`iddoctor`)
    REFERENCES `tcs_clinic`.`person` (`idperson`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `tcs_clinic`.`patient`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs_clinic`.`patient` (
  `idpatient` INT NOT NULL,
  `father` VARCHAR(45) NULL,
  `mother` VARCHAR(45) NULL,
  `hypertensive` VARCHAR(45) NULL,
  INDEX `fk_paciente_pessoa_idx` (`idpatient` ASC),
  PRIMARY KEY (`idpatient`),
  CONSTRAINT `fk_paciente_pessoa`
    FOREIGN KEY (`idpatient`)
    REFERENCES `tcs_clinic`.`person` (`idperson`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `tcs_clinic`.`appointment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs_clinic`.`appointment` (
  `idappointment` INT NOT NULL,
  `idpatient` INT NOT NULL,
  `iddoctor` INT NOT NULL,
  `date` DATETIME NULL,
  `diagnosis` VARCHAR(150) NULL,
  `prescription` VARCHAR(150) NULL,
  PRIMARY KEY (`idappointment`),
  INDEX `fk_consulta_paciente1_idx` (`idpatient` ASC) ,
  INDEX `fk_consulta_medico1_idx` (`iddoctor` ASC) ,
  CONSTRAINT `fk_consulta_paciente1`
    FOREIGN KEY (`idpatient`)
    REFERENCES `tcs_clinic`.`patient` (`idpatient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_consulta_medico1`
    FOREIGN KEY (`iddoctor`)
    REFERENCES `tcs_clinic`.`doctor` (`iddoctor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `tcs_clinic`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `tcs_clinic`.`user` (
  `iduser` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(45) NULL,
  `password` VARCHAR(45) NULL,
  PRIMARY KEY (`iduser`),
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) )
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
