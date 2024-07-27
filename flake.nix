{
  description = "A very basic flake";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-24.05";
  };

  outputs = { self, nixpkgs }:
    let
      pkgs = nixpkgs.legacyPackages.x86_64-linux;
    in
      {

        packages.x86_64-linux.ghostspawner = pkgs.stdenv.mkDerivation {
          pname = "GhostSpawner";
          version = "v0.1";
          src = ./.;
          nativeBuildInputs = with pkgs;[
            SDL2
            SDL2_ttf
            SDL2_image
            SDL2_mixer
          ];
          buildInputs = with pkgs;[
            #dotnet-sdk
            mono
          ];

          buildPhase = ''
          runHook preBuild
          xbuild /p:Configuration=Release "GhostSpawner.sln"
          runHook postBuild
                 '';
          installPhase = ''
            mkdir $out
            ls
            cp -r Game/bin/Release/* $out
                        '';
        } ;

    packages.x86_64-linux.default = self.packages.x86_64-linux.ghostspawner;

    # devShells.x86_64-linux.default = pkgs.mkShell {
    #   name = "dotnet-shell";
    #   packages = with pkgs;[
    #     dotnet-sdk
    #     mono
    #   ];
    # };

  };
}
