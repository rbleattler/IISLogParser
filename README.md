<div id="top"></div>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">IISLogParser</h3>

  <p align="center">
    Parsing IIS logs into .Net List<>  !
    <br />
    ·
    <a href="https://github.com/Kabindas/IISLogParser/issues">Report Bug</a>
    ·
    <a href="https://github.com/Kabindas/IISLogParser/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#processing">Processing</a></li>
    <li><a href="#using">Using</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This library allows the import and parsing of IIS log files from the filesystem to a List . This project arose from the need to have a component that allows both the import of small log files as well as larger files (> 1Gb).

Build with NET Standard 2.0.3, can be used with .Net Framework or .Net Core

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

List of frameworks/libraries used.

* [.netstandard2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0)
* [Best-README-Template](https://github.com/othneildrew/Best-README-Template)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/Kabindas/IISLogParser.git
   ```
   
<p align="right">(<a href="#top">back to top</a>)</p>

<!-- Processing -->
## Processing

The processing engine detects the size of the file to be processed and if it is less than 50 Mb it does a single read to memory and treats the data from there, for larger files to avoid OutOfMemory, reading is done line by line.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- Using -->
## Using

**FilePath**  [_string_] -> Path to the file

**MissingRecords**  [_bool_] -> Flag that indicates if there are any missing records. For larger files, this property can be flagged as true.

**CurrentFileRecord**  [_int_] -> When processing large files this will store the currently file record index. For example, if the file has 1.000.000 log events and the processing is done in blocks of 250000(_MaxFileRecord2Read_), we'll have 4 cycles each on with this flag set with 250.000, 500.000, 750.000 and finally 1.000.000

**MaxFileRecord2Read**  [_int_] -> Controls the maximum limit of items that the List can have. If the number of events in the log file exceeds MaxFileRecord2Read the MissingRecords variable assumes the value of true and we can perform one more reading of a MaxFileRecord2Read set. For files less than 50Mb this value has no effect because the engine performs a single read to memory and treats the data from there. For example, if the file has 1.000.000 log events and this is set to 250.000, will perform 4 cycle each one extracting a List with a count of 250.000

Usage :

```
        List<IISLogEvent> logs = new List<IISLogEvent>();
        using (ParserEngine parser = new ParserEngine([filepath]))
        {
            while (parser.MissingRecords)
            {
                logs = parser.ParseLog().ToList();
            }
        }
```

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Vitor Correia <i>aka</i> Kabindas - vitormiguelcorreia@gmail.com

Project Link: [https://github.com/Kabindas/IISLogParser](https://github.com/Kabindas/IISLogParser)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Kabindas/IISLogParser.svg?style=for-the-badge
[contributors-url]: https://github.com/Kabindas/IISLogParser/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Kabindas/IISLogParser.svg?style=for-the-badge
[forks-url]: https://github.com/Kabindas/IISLogParser/network/members
[stars-shield]: https://img.shields.io/github/stars/Kabindas/IISLogParser.svg?style=for-the-badge
[stars-url]: https://github.com/Kabindas/IISLogParser/stargazers
[issues-shield]: https://img.shields.io/github/issues/Kabindas/IISLogParser.svg?style=for-the-badge
[issues-url]: https://github.com/Kabindas/IISLogParser/issues
[license-shield]: https://img.shields.io/github/license/Kabindas/IISLogParser.svg?style=for-the-badge
[license-url]: https://github.com/Kabindas/IISLogParser/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/vitormiguelcorreia/
