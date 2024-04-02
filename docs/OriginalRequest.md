# Mangopay Technical Test

## Introduction
*PLEASE CAREFULLY READ THE FOLLOWING README IN ITS ENTIRETY, AS EACH INSTRUCTION IS CRITICAL*.

This README is written in markdown language, employing formats that aid in understanding specific key points.

We recommend using a tool like VS Code or similar to preview this document.


## Getting Started
- Clone the repository from the bundle, using  
`git clone -b master tech-test-v2.bundle tech-test`  
The tech-test behaves like a standard git repository. You can commit, rebase your commits, and so on.  
- Make a separate commit for each step
- Upon completion, execute the following command from your repository:  
`git bundle create firstname.lastname.bundle master`
- You can verify the content of your bundle using  
`git bundle verify firstname.lastname.bundle`  
The output should resemble:
    ```firstname.lastname.bundle is okay
    The bundle contains this ref:
    d99f1f1e0cef92c2b493dc44ee48e14488ba2fee refs/heads/master
    The bundle records a complete history.
    ```
    Your bundle will be unbundled using the following command:  
`git fetch ../firstname.lastname.bundle master:candidate/firstname.lastname`
- Forward your bundle to the recruiter

## Technical Context
You have access to the `Payment.Core` project, which outlines the interfaces of the model and service classes you will need to **implement or rectify**.  
- **Existing interfaces** in this project (*Payment.Core*) should not require modification.  
- However, you are free to augment this project as needed with new features
- You also have the liberty to remove, modify, or add anything to the solution if deemed necessary.

You also have at your disposal, the `Payment.Test` project which includes all the unit tests that need to pass, organised in steps. This also aids in better understanding the application's functionalities.

## Functional Context
This software solution should be able to manage users as outlined in `IUserService`. This includes:
- Adding, deleting, and fetching user data
- Establishing friendships between two users
- Retrieving common friends between two users
- Determining the shortest path (if one exists) between two users via their mutual friends

The solution should also manage user wallets as described in `IWalletService`. This includes:
- Creating and retrieving wallets
- Making contributions to a wallet, which involves two aspects. First, increasing the wallet amount. Second, attaching a `Share` to it which denotes user contribution to that wallet. Please note, **a contribution to a wallet must be made in the same currency**

## Your Objectives

### Mandatory
1. Ensure all the tests from **Step1.UserServiceTests** pass. 
> *#Tips: You may need to amend the existing implementation! Do not hesitate to tweak or add lines as needed.*
2. Ensure the code maintains high standards. Can you spot any code smells? Can we optimise this code? 
> **#Tips: Your code should be extensible and demonstrate low implementation coupling**.
3. Ensure all the tests from **Step2.WalletServiceTests** pass

### Optional
4. Ensure all the tests from **Step3.GetConnectionTests** pass

Remember to make a specific commit for each step.