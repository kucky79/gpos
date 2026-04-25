# GPOS / Bifrost Framework

엔터프라이즈 한국어 **POS(Point-of-Sale) 및 매장관리 시스템**입니다.
.NET Framework 4.8 / WinForms / DevExpress 20.1 / SQL Server 기반으로 동작하며,
공용 프레임워크인 **Bifrost** 위에 다수의 업무 모듈(DLL)을 동적으로 로드하는 플러그인 구조를 가집니다.

> 자세한 구조와 계층 설계는 [`architecture.md`](./architecture.md) 를 참고하세요.

---

## 주요 응용 프로그램

| 실행 파일 | 역할 |
|---|---|
| **Ether.exe** | 메인 POS 단말 (매출/매입/재고/조회) |
| **Jarvis.exe** | 마스터 데이터 · 시스템 관리 콘솔 |
| **AppStarter.exe** | 자동 업데이트 · 런처 |
| **ReportDesigner.exe** | DevExpress 기반 리포트 디자인 도구 |

---

## 디렉터리 구조

```
GPOS/
├── Bifrost_Framework.sln    # 통합 솔루션 (67개 프로젝트)
├── Framework/               # 공용 Bifrost.* 프레임워크 (18 프로젝트)
├── ExecuteProgram/          # 실행 프로그램 4종
├── POS/                     # POS 업무 모듈 (M_POS_*) — 34개
├── MAS/                     # 마스터 데이터 모듈 (M_MAS_*) — 5개
├── SYS/                     # 시스템/권한 모듈 (M_SYS_*) — 2개
├── architecture.md          # 아키텍처 문서
└── README.md
```

---

## 기술 스택

- **.NET Framework 4.8**, C#
- **WinForms** + **DevExpress 20.1.3.0** (XtraEditors / XtraGrid / XtraBars / XtraLayout / XtraPrinting)
- **Microsoft SQL Server** — Stored Procedure 기반, ORM 미사용
- **NuGet:** `Neodynamic.SDK.ThermalLabel` 9.0.19.1114, `SkiaSharp` 1.60.0, `HarfBuzzSharp` 1.4.6, `System.Text.Encoding.CodePages` 4.4.0
- **인쇄:** Neodynamic ThermalLabel + SkiaSharp
- **다국어:** 한국어(ko-KR) 기본 / 일본어 · 중국어 · 영어 지원
- **업데이트:** ASMX(SOAP) `AppUpdaterService` + `Bifrost.FileTransfer`

---

## 사전 준비

### 개발 환경
- Windows 10 / 11
- **Visual Studio 2019** 이상 (.NET desktop development 워크로드)
- **.NET Framework 4.8 Developer Pack**
- **DevExpress 20.1.3.0** — 빌드 머신에 라이선스 설치 필요 (NuGet으로 제공되지 않음)

### 운영 환경
- Windows 10 / 11
- Microsoft SQL Server 2016 이상
- DevExpress 런타임 (배포 패키지에 포함)

---

## 빌드 방법

```cmd
:: 솔루션 폴더에서
nuget restore Bifrost_Framework.sln
msbuild Bifrost_Framework.sln /p:Configuration=Release /m
```

또는 Visual Studio 에서 `Bifrost_Framework.sln` 을 열고 **Build Solution (Ctrl + Shift + B)**.

### 빌드 순서

빌드는 솔루션 의존 관계에 따라 자동으로 다음 순서로 수행됩니다.

1. `Framework/Bifrost.*` (공용 라이브러리)
2. `POS/M_POS_*`, `MAS/M_MAS_*`, `SYS/M_SYS_*` (업무 모듈 DLL)
3. `ExecuteProgram/*` (실행 응용 프로그램)

### 빌드 산출물

각 프로젝트의 Post-Build 이벤트는 결과물을 **`D:\GPOSDeploy`** 로 복사합니다.
(운영 환경 클라이언트 폴더와 동일한 구조)

> 💡 자신의 환경에 `D:\GPOSDeploy` 폴더가 없으면 빌드 후처리에서 오류가 날 수 있습니다.
> 미리 폴더를 생성하거나, 필요 시 Post-Build Event 의 경로를 자신의 경로로 수정하세요.

---

## 실행 / 환경 설정

### Setting.ini

각 실행 응용 프로그램(`Ether`, `Jarvis`, …) 폴더에 **`Setting.ini`** 파일이 필요합니다.

```ini
[DB]
ConnectionString=<DES 암호화된 연결 문자열>
```

- 알고리즘: **DES**
- 키: `greenpos`
- 평문 예시: `Data Source=localhost;initial Catalog=GPOS;uid=YOUR_USER;password=YOUR_PASSWORD;`

> 보안상 `Setting.ini` 와 평문 비밀번호는 **절대 저장소에 커밋하지 마세요.**

### 실행 순서

1. **AppStarter.exe** 실행 → 버전 확인 및 갱신
2. AppStarter 가 **Ether.exe** (또는 관리자는 **Jarvis.exe**) 를 기동
3. 로그인 화면에서 사용자 인증 후 메인 MDI 창 진입

---

## 신규 모듈 추가 가이드

새로운 업무 화면을 모듈 DLL 로 추가할 때:

1. 카테고리에 맞는 폴더에 프로젝트 생성
   - POS 화면 → `POS\M_POS_<NAME>\`
   - 마스터 → `MAS\M_MAS_<NAME>\`
   - 시스템 → `SYS\M_SYS_<NAME>\`
2. 솔루션에 추가 (`Bifrost_Framework.sln`)
3. 다음 참조 추가:
   - `Bifrost.Common`, `Bifrost.Data`, `Bifrost.DBManager`
   - `Bifrost.Win`, `Bifrost.Win.Controls`
   - 필요 시 `Bifrost.Helper`, `Bifrost.POSPopup`, `Bifrost.Grid`
4. 메인 폼은 **`POSFormBase`** 또는 **`BifrostFormBase`** 를 상속
5. 메뉴/권한 등록 — `M_MAS_CODE` / `M_SYS_USER` 데이터 셋업
6. Post-Build 이벤트에 `D:\GPOSDeploy` 복사 추가
7. 실행 프로그램(Ether/Jarvis) 의 메뉴에서 모듈 키 매핑

---

## 주요 디자인 결정

- **Stored Procedure 중심** — 모든 데이터 접근은 SP 경유 (ORM 없음)
- **DataTable 직접 바인딩** — 도메인 모델 대신 ADO.NET `DataTable` 사용
- **모듈 = DLL** — 업무 단위로 독립 컴파일 / 동적 로드
- **DevExpress RibbonForm** 베이스의 통일된 UX
- **DES 암호화 ConnectionString** — `greenpos` 키 사용

자세한 내용과 한계점은 [`architecture.md`](./architecture.md) §11 참고.

---

## 보안 / 커밋 시 주의

다음 항목은 **저장소에 커밋하지 않습니다** (`.gitignore` 에 이미 등록):

- `bin/`, `obj/`, `Debug/`, `Release/`, `x64/`, `x86/`
- `packages/` (NuGet 캐시)
- `GPOSDeploy/` (빌드 산출물)
- `*.dll`, `*.exe`, `*.pdb`, `*.cache`, `*.log`
- `*.user`, `*.suo`, `.vs/`
- `*.zip`, `*.bak`

추가로 다음은 **수동으로 확인 후 커밋 금지**:

- `Setting.ini` (DB 연결 문자열 포함)
- `*.snk` 파일은 필요시 별도 관리 (현재 `kucky.snk` 사용)
- 운영 환경 패스워드 / API 키

---

## 라이선스 / 권리

본 저장소는 사내 / 프로젝트 전용 코드베이스입니다.
DevExpress, Neodynamic ThermalLabel SDK 등 서드파티 컴포넌트는 각각의 라이선스를 따릅니다.
