# GPOS / Bifrost Framework — 아키텍처 문서

## 1. 개요

GPOS는 **Bifrost Framework** 위에 구축된 엔터프라이즈급 한국어 POS(Point-of-Sale) 및 매장관리 시스템입니다.
.NET Framework 4.8 기반의 WinForms 응용 프로그램으로, DevExpress 20.1 컨트롤과 SQL Server 백엔드를 사용합니다.

- **솔루션:** `Bifrost_Framework.sln` (총 67개 C# 프로젝트)
- **타깃 프레임워크:** .NET Framework 4.8
- **언어/문화권:** ko-KR (한국어 기본)
- **UI 라이브러리:** DevExpress 20.1.3.0 (XtraEditors / XtraGrid / XtraBars / XtraLayout / XtraPrinting)
- **DB:** Microsoft SQL Server (Stored Procedure 기반, ORM 미사용)
- **배포 산출물:** `D:\GPOSDeploy` (Post-Build 이벤트로 복사)

---

## 2. 솔루션 구성

```
E:\source\GPOS\
├── Bifrost_Framework.sln          # 통합 솔루션
├── Framework\                     # 공용 프레임워크 (18개 프로젝트)
├── ExecuteProgram\                # 실행 가능 응용 프로그램 (4개)
│   ├── Ether\                     # ▶ 메인 POS 단말 (WinExe)
│   ├── Jarvis\                    # ▶ 관리/마스터 콘솔 (WinExe)
│   ├── AppStarter\                # ▶ 자동 업데이트/런처 (WinExe)
│   └── ReportDesigner\            # ▶ 리포트 디자이너 (WinExe)
├── POS\                           # POS 업무 모듈 DLL (34개)
├── MAS\                           # 마스터 데이터 모듈 DLL (5개)
├── SYS\                           # 시스템 관리 모듈 DLL (2개)
├── GPOSDeploy\                    # 로컬 빌드 산출물 (git ignore)
└── packages\                      # NuGet 캐시 (git ignore)
```

---

## 3. 계층 아키텍처

```
┌──────────────────────────────────────────────────────────────┐
│  Presentation Layer (WinForms + DevExpress)                  │
│  Ether.exe / Jarvis.exe  ←  M_POS_* / M_MAS_* / M_SYS_* DLL  │
│                              (모듈 동적 로딩)                │
└──────────────────────────────────────────────────────────────┘
                              ▲
                              │ 상속 / 참조
                              ▼
┌──────────────────────────────────────────────────────────────┐
│  Framework Layer (Bifrost.*)                                 │
│  ┌──────────────┬──────────────┬───────────────────────────┐ │
│  │ Bifrost.Win  │ Bifrost.*    │ Bifrost.Framework.BSLBase │ │
│  │  FormBase    │  Controls    │ Bifrost.Framework.DSLBase │ │
│  │  POSFormBase │  Grid/Popup  │  (기반 클래스 / 로깅)     │ │
│  └──────────────┴──────────────┴───────────────────────────┘ │
└──────────────────────────────────────────────────────────────┘
                              ▲
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│  Data Access Layer                                           │
│  Bifrost.DBManager  →  DbAgent (Singleton, ADO.NET, SP-driven)│
│                        ParameterCache / SpInfoCollection      │
│  Setting.ini  →  DES 암호화된 ConnectionString               │
└──────────────────────────────────────────────────────────────┘
                              ▲
                              │
                              ▼
┌──────────────────────────────────────────────────────────────┐
│  Microsoft SQL Server (Stored Procedure 중심 스키마)         │
└──────────────────────────────────────────────────────────────┘
```

> **비고:** 비즈니스 로직 대부분이 폼 코드비하인드(Form code-behind)에 위치합니다.
> `BSLBase` / `DSLBase`는 로깅 및 식별용 베이스 클래스 수준이며, 본격적인
> 도메인 모델은 사용하지 않고 `DataTable` / `DataSet`을 직접 바인딩합니다.

---

## 4. Framework 프로젝트 (Bifrost.*)

| 프로젝트 | 역할 |
|---|---|
| **Bifrost.Common** | 공용 enum (`MessageType`, `EditAction`, `ResourceType`), 메시지 리소스, `AppConfigReader`, 폼 설정 영속화 |
| **Bifrost.Data** | `GlobalData`, `MenuData`, `MenuAccessData`, `POSGlobalData` 전역 상태 / 다국어 리소스(ResX) |
| **Bifrost.System** | 암복호화(`SimpleEncryptor`), 시스템 유틸리티 |
| **Bifrost.DBManager** | **DB 접근의 핵심.** `DbAgent`(Singleton), `DB` Facade, `SpInfo`/`ParameterCache`. SP 파라미터 캐시 + 명시적 트랜잭션 관리 |
| **Bifrost.Win** | `FormBase`(DevExpress RibbonForm 상속) + `POSFormBase`(EditAction, 검색/신규/저장/삭제/인쇄 버튼, 상태바 관리) |
| **Bifrost.Win.Controls** | DevExpress 기반 커스텀 WinForms 컨트롤 |
| **Bifrost.Adv.Controls** | 고급 그리드/에디터 컨트롤 |
| **Bifrost.Grid** | XtraGrid 래퍼 |
| **Bifrost.Helper** | `SetControl`(콤보/룩업 동적 채움), 암호화, 일반 유틸 |
| **Bifrost.CommonPopup / Bifrost.POSPopup** | 공통/POS 전용 팝업 다이얼로그 |
| **Bifrost.Office** | Excel / PDF 연동 |
| **Bifrost.FileTransfer.Client / Server** | 클라이언트–서버 파일 전송 (업데이트, 리포트 송수신) |
| **Bifrost.Framework.BSLBase** | 비즈니스 로직 베이스 (`SubSystemType` enum, 타임스탬프 로깅) |
| **Bifrost.Framework.DSLBase** | 데이터 서비스 레이어 베이스 |
| **Bifrost.SY.BSL.SYS / Bifrost.SY.DSL.SYS** | 시스템 모듈(사용자/권한)의 BSL/DSL |

---

## 5. 실행 프로그램 (ExecuteProgram)

### 5.1 Ether — 메인 POS 단말
- **타입:** WinExe (.NET 4.8), MDI 기반
- **주요 폼:** `MainForm`, `LoginForm`, `SettingForm`
- **기능:** 로그인 → 메뉴/모듈 동적 로딩 → 매출/매입/마스터 화면 호출
- **특징:** 터치 UI 최적화, 한국어 폰트(본고딕KR / 카이겐고딕 / D2Coding) 임베드
- **업데이트 채널:** `AppUpdaterService` (ASMX SOAP)
- **시작 시퀀스:** 한국어 컬쳐 설정 → DevExpress 스킨 적용 → `MainForm` 표시

### 5.2 Jarvis — 관리/마스터 콘솔
- **타입:** WinExe (.NET 4.8), MDI
- **역할:** 마스터 데이터 관리, 시스템 설정, 리포트 (M_MAS_* / M_SYS_* 모듈 호출)
- 구조는 Ether와 거의 동일하며 로딩되는 모듈군이 다름

### 5.3 AppStarter — 런처 / 자동 업데이트
- **타입:** WinExe (.NET 4.8)
- **역할:** Ether 실행 전 버전 체크 → `Bifrost.FileTransfer.Client`로 파일 다운로드 → Ether 기동
- **주요 폼:** `UpdateProgForm`

### 5.4 ReportDesigner
- **타입:** WinExe (.NET 4.8)
- **역할:** DevExpress Reporting 기반 리포트 디자인 도구

---

## 6. 업무 모듈 (Plugin DLL)

각 모듈은 **독립적인 클래스 라이브러리(.dll)** 로 컴파일되며, 메인 응용 프로그램의 메뉴 시스템을 통해 동적으로 로드되고 MDI 자식 폼으로 표시됩니다.
모든 모듈 폼은 `Bifrost.Win.POSFormBase`(또는 `BifrostFormBase`)를 상속합니다.

### 6.1 명명 규칙

| 접두사 | 영역 | 위치 |
|---|---|---|
| `M_POS_*` | POS 거래/조회 | `POS\` |
| `M_MAS_*` | Master Data | `MAS\` |
| `M_SYS_*` | System / 권한 | `SYS\` |

### 6.2 POS 모듈 (34개)

| 분류 | 모듈 |
|---|---|
| **거래 입력** | `M_POS_SALE` (매출 — 핵심), `M_POS_PURCHASE` (매입) |
| **마스터** | `M_POS_ITEM`, `M_POS_ITEMTYPE`, `M_POS_ITEMUNIT`, `M_POS_CUST`, `M_POS_STORE`, `M_POS_MENU`, `M_POS_CODE`, `M_POS_CONTENTS_POSITION` |
| **인쇄** | `M_POS_PRINT`, `M_POS_PRINT_CONFIG`, `M_POS_SALE_RECEIPT` |
| **재고** | `M_POS_INV_OPEN`, `M_POS_INV_SEARCH`, `M_POS_INV_SUM` |
| **조회/검색** | `M_POS_SALE_SEARCH01/02`, `M_POS_PURCHASE_SEARCH/02`, `M_POS_BILL_SEARCH`, `M_POS_CLS_SEARCH`, `M_POS_PROFIT_SEARCH`, `M_POS_NONPAID_SEARCH`, `M_POS_NONPAID_SEARCH_PO`, `M_POS_PO_SEARCH01`, `M_POS_SO_SEARCH01`, `M_POS_ITEM_SALE_SEARCH01`, `M_POS_ITEM_PURCHASE_SEARCH01` |
| **마이그레이션/레거시** | `M_POS_MIGRATION`, `M_POS_OLD01-04`, `M_POS_OLD51-54`, `M_POS_OLD_SALE`, `M_POS_SALE_OLD` |

### 6.3 MAS 모듈 (5개)

`M_MAS_CODE` · `M_MAS_STORE_MANAGE` · `M_MAS_STORE_MIGRATION` · `M_MAS_EMP` · `M_MAS_NOTICE`

### 6.4 SYS 모듈 (2개)

`M_SYS_USER`(사용자/권한) · `M_SYS_CONFIG`(시스템 설정)

### 6.5 모듈 표준 패턴

```csharp
public partial class FormSale : POSFormBase
{
    private DataTable dtH;    // 헤더 (Header)
    private DataTable dtL;    // 라인 (Detail / Line)
    private DataTable dtPay;  // 결제 수단 (Payment)

    // 그리드 바인딩
    gridControl.DataSource = dtL;

    // SP 호출
    ResultData rd = DB.GetInstance().FillResultTable("usp_xxx", paras);
    DataTable dt = rd.DataTable;
}
```

- 헤더–라인–결제 3-DataTable 구조가 거래성 모듈의 표준
- 트랜잭션은 `DbAgent`의 명시적 `BeginTransaction` / `CommitTransaction` 으로 제어
- 모든 룩업/콤보는 `Bifrost.Helper.SetControl` + 코드 마스터(`M_MAS_CODE`)에서 채움

---

## 7. 데이터 접근

- **방식:** ADO.NET + Stored Procedure (ORM 미사용)
- **연결 문자열:** 응용 프로그램 폴더의 `Setting.ini` `[DB] ConnectionString=` 항목에 저장하며 **DES 암호화** (key: `greenpos`)
- **기본값:** `Data Source=localhost; initial Catalog=GPOS; uid=YOUR_USER; password=YOUR_PASSWORD;`
- **격리 수준:** `ReadCommitted`
- **싱글턴 진입점:** `DB.GetInstance()` → 내부적으로 `DbAgent` 사용
- **파라미터 캐시:** SP 메타데이터(`SpInfoCollection`)를 캐싱하여 호출 비용 절감
- **트랜잭션:** 코드비하인드에서 명시적 begin/commit/rollback

---

## 8. 인증 / 업데이트 / 외부 연동

| 영역 | 방식 |
|---|---|
| **로그인** | `LoginForm` → `M_SYS_USER` BSL/DSL → SQL Server 사용자 테이블 |
| **권한** | `MenuAccessData` 가 메뉴별 접근 권한 보유 |
| **업데이트 서비스** | ASMX SOAP `AppUpdaterService.AppUpdater.asmx` (AppStarter 내 Web Reference) |
| **파일 송수신** | `Bifrost.FileTransfer.Client/Server` (업데이트, 리포트, 마이그레이션) |
| **인쇄(영수증/라벨)** | `Neodynamic.SDK.ThermalLabel` 9.0.19.1114 + `SkiaSharp` 1.60.0 |
| **Office 연동** | `Bifrost.Office` (Excel 출력, PDF) |
| **Win32 Interop** | `User32.dll` (윈도우 핸들/포커스 제어) |

---

## 9. 빌드 & 배포

### 9.1 빌드 순서

1. **Framework 프로젝트** — 다른 모든 프로젝트의 공통 의존성이므로 먼저 빌드
2. **POS / MAS / SYS 모듈 DLL** — Framework를 참조하여 빌드
3. **ExecuteProgram (Ether / Jarvis / AppStarter / ReportDesigner)** — 모든 모듈을 참조

### 9.2 산출물 배포

- 각 csproj의 **Post-Build Event** 가 결과물을 `D:\GPOSDeploy` 로 복사
- 운영 환경에서는 이 폴더가 곧 클라이언트 설치 폴더
- **AppStarter** 가 이 폴더를 기준으로 신규 파일을 받아 갱신

### 9.3 코드 사이닝

- `Bifrost.Data`, `Bifrost.DBManager` 는 `kucky.snk` 로 Strong Name 서명
- Public Key Token: `b8524490ef01034a`

### 9.4 NuGet 패키지

| 패키지 | 버전 | 용도 |
|---|---|---|
| `Neodynamic.SDK.ThermalLabel` | 9.0.19.1114 | 영수증/라벨 인쇄 |
| `SkiaSharp` | 1.60.0 | 2D 그래픽 |
| `HarfBuzzSharp` | 1.4.6 | 텍스트 셰이핑 |
| `System.Text.Encoding.CodePages` | 4.4.0 | 레거시 코드 페이지 |

> DevExpress 20.1 은 NuGet이 아닌 GAC / 외부 참조로 해결됨 — 빌드 머신에 별도 설치 필요

---

## 10. 디렉터리 / 파일 컨벤션 요약

| 항목 | 위치 / 규칙 |
|---|---|
| 솔루션 | 루트 `Bifrost_Framework.sln` |
| Framework DLL | `Framework\Bifrost.<Name>\` |
| 응용 프로그램 | `ExecuteProgram\<AppName>\` |
| 업무 모듈 | `POS\M_POS_*` · `MAS\M_MAS_*` · `SYS\M_SYS_*` |
| 빌드 산출물 | `D:\GPOSDeploy` (post-build) / `bin/`, `obj/` (gitignore) |
| 환경 설정 | 각 응용 폴더의 `Setting.ini` (DES 암호화) |
| 리소스 | SVG 아이콘 (40+), 한국어 폰트(본고딕KR), ResX 메시지 |

---

## 11. 알려진 한계 / 개선 여지

- **계층 분리 약함** — 비즈니스 로직 다수가 폼 코드비하인드에 존재
- **DI / IoC 미적용** — 싱글턴(`DB.GetInstance()`) 직접 사용
- **도메인 모델 부재** — `DataTable` 직접 바인딩, 단위 테스트 어려움
- **레거시 모듈 잔존** — `M_POS_OLD*`, `M_POS_SALE_OLD` 등 비활성 모듈 다수
- **DevExpress 20.1 강결합** — 업그레이드 시 광범위한 영향
- **ASMX (SOAP)** — 업데이트 서비스가 레거시 프로토콜
- **하드코딩된 배포 경로** — `D:\GPOSDeploy` (개발/CI 머신 환경 의존)

---

## 12. 컴포넌트 의존 관계 (요약)

```
                ┌─────────────────────────┐
                │  Ether.exe / Jarvis.exe │
                └─────────────┬───────────┘
                              │ 동적 로드
        ┌─────────────────────┼─────────────────────┐
        ▼                     ▼                     ▼
   M_POS_*.dll           M_MAS_*.dll           M_SYS_*.dll
        │                     │                     │
        └─────────────────────┼─────────────────────┘
                              ▼
                Bifrost.Win.* / Bifrost.*.Controls
                              │
                              ▼
              Bifrost.Helper / Bifrost.Common / Bifrost.Data
                              │
                              ▼
                     Bifrost.DBManager
                              │
                              ▼
                       SQL Server (SP)
```
