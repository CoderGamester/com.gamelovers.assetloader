# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.4.0] - 2020-07-26

- Added *AddressablesLoader* to the samples package with the asset loader implementation for the asset loader

**Changed**
- Removed the dependency to Unity's Addressables package
- Removed the Addressables helper methods from *AssetLoaderUtils* to remove any external package dependency
- Removed the Addressables editor build method
- Moved *AddressablesIdsGenerator* to the samples package

## [0.3.0] - 2020-07-13

- Added the *IAssetLoader* to allows to wrap the Addressables Loading scheme into an object reference

**Changed**
- Renamed the *AssetLoaderService* to *AssetLoaderUtils*

## [0.2.0] - 2020-02-26

- Added scene loading to the *AssetLoaderService*
- Added the possibility to save scenes configs in the *AddressableConfig*

## [0.1.1] - 2020-02-15

- Updated package version and dependencies version

## [0.1.0] - 2020-02-15

- Initial submission for package distribution
