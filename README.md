<img src="https://github.com/RHEAGROUP/CDP4-COMET-HERMES/raw/development/COMET-Community-Edition.png" width="250">

**CDP4-COMET HERMES** is an open-source web application used to synchronize data between multiple E-TM-10-25 data sources. It can be used to synchronize the following types of data:
  - SiteDirectory: DomainOfExpertise, Organization, NaturalLanguage, etc...
  - Reference Data: Annex B populations (ParameterTypes, MeasurementScales, MeasurementUnits, etc...)
  - Model Data: synchronize the structure of models (ElementDefinitions, Parameters, States, etc...)

A demo version of the application is available at https://hermes.mbsehub.org

![GitHub issues](https://img.shields.io/github/issues/RHEAGROUP/CDP4-COMET-HERMES.svg)

## Build status

GitHub actions are used to build and test the software

Branch | Build Status
------- | :------------
Master | ![Build Status](https://github.com/RHEAGROUP/CDP4-COMET-HERMES/actions/workflows/CodeQuality.yml/badge.svg?branch=master)
Development | ![Build Status](https://github.com/RHEAGROUP/CDP4-COMET-HERMES/actions/workflows/CodeQuality.yml/badge.svg?branch=development)

> HERMES is automaticaly deployed to https://hermes.mbsehub.org using a [Github action](https://github.com/RHEAGROUP/CDP4-COMET-HERMES/actions/workflows/publish-docker-container.yml)

## SonarQube Status:

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=alert_status)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=security_rating)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=coverage)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=bugs)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=ncloc)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=sqale_index)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_CDP4-COMET-HERMES&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=RHEAGROUP_CDP4-COMET-HERMES)

# License

The software contained in the CDP4-COMET-HERMES solution is provided to the community under the APACHE 2.0 License. Because of this, it can be used in both open source and proprietary software without being required to release the source code of your own components.

# Contributions

Contributions to the code-base are welcome. However, before we can accept your contributions we ask any contributor to sign the Contributor License Agreement (CLA) and send this digitaly signed to s.gerene@rheagroup.com. You can find the CLA's in the CLA folder.