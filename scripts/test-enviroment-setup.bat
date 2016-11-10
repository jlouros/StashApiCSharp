REM script.bat! Trying to automate the steps described above with in a batch script

cd c:\Atlassian\code\

mkdir TestRepository
cd TestRepository
echo.initial set of text>test.txt
mkdir folder
cd folder
echo.in subfolder>test.txt
cd ..
mkdir "my folder"
cd "my folder"
echo.in subfolder with spaces>"my test.txt"

git init
git add --all
git commit -m "Initial Commit"
git remote add origin http://TestUser@localhost:7990/scm/test/testrepository.git
git push -u origin master

git tag -a TestTag -m "my test tag"
git push origin --tags

echo.more text>>test.txt
git add --all
git commit -m "small change"

git branch develop
git checkout develop

echo.even more text>>test.txt
git add --all
git commit -m "develop change"
git push --set-upstream origin develop