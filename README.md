# Overview

This application demonstrate connect to GitHub repository and access all the commmit comments belong to the repository.

# GitHubApiAccess

This folder contains source code for both the application as well as unit testing.

# GitHubAccessExe

This folder contains the working exe version build of the project.

# Documentation

This folder contains a detail architecture design as well as key objectives and multiple screenshots in one place with sample input to run the application as well.

# Observation

It has been observed that for security reason GitHub automatically revokes the generated Personal Access Token if it is mentioned anywhere in the code or document during commit.
The token mentioned in sample input is no longer valid due to this. This application can run on any Github repository which attached to a valid personal access token of user.



