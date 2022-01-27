#!/bin/bash
git branch -d $1
git push origin --delete $1
